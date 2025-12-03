using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Products.DTOs;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed class UpdateReturnCommandHandler(IReturnRepository returnRepository, IProductList productList) : IRequestHandler<UpdateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IProductList _productList = productList;

    public async Task<ValidationResult> Handle(UpdateReturnCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products.Select(p => new LineItemDto(p.ProductId, p.Quantity)), cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

        var returnEntity = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnEntity is null)
            return new ValidationResult([new ValidationFailure(Resource.RETURN, string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId))]);

        var returnProducts = productsList.Select(p => new ReturnProduct(request.ReturnId, p.ProductId, p.Quantity, p.SellingPrice, request.Products.Select(p => p.Loss).FirstOrDefault())).ToList();

        returnEntity.Update(
            request.Justification,
            request.InvoiceNumber,
            request.UserId,
            request.ClientId,
            returnProducts);

        var result = await _returnRepository.UpdateAsync(returnEntity, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _returnRepository.CommitAsync(cancellationToken);
        if (!commitResult)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return result;

    }
}