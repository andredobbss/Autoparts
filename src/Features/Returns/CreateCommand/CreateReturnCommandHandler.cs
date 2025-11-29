using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed class CreateReturnCommandHandler(IReturnRepository returnRepository, IProductList productList) : IRequestHandler<CreateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(CreateReturnCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(nameof(request.Products), Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(nameof(productsList), Resource.PRODUCTS_NOT_FOUND)]);

        Guid returnId = Guid.NewGuid();

        var returnProducts = productsList
            .Select(product => new ReturnProduct(returnId, product.ProductId, product.Quantity, product.SellingPrice, request.Loss))
            .ToList();

        var returnEntity = new Return(returnId, request.Justification, request.InvoiceNumber, request.UserId, request.ClientId, returnProducts);

        var result = await _returnRepository.AddAsync(returnEntity, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _returnRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;


    }
}