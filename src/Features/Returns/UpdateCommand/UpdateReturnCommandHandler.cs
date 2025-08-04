using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Products.Stock;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;
public sealed class UpdateReturnCommandHandler(IReturnRepository returnRepository, IStockCalculator stockCalculator, IProductList productList) : IRequestHandler<UpdateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    private readonly IProductList _productList = productList;

    public async Task<ValidationResult> Handle(UpdateReturnCommand request, CancellationToken cancellationToken)
    {
        var returnEntity = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnEntity is null)
            return new ValidationResult { Errors = { new ValidationFailure("Return", $"{Resource.ID_NOT_FOUND} : {request.ReturnId}") } };

        var productsList = await _productList.GetProductsListAsync(request.Products.Select(r => new SharedProductsDto(request.Products.Select(r => r.ProductId).First(), request.Products.Select(r => r.Quantity).First())), cancellationToken);

        var returnProducts = productsList.Select(p => new ReturnProduct(request.ReturnId, p.ProductId, p.Quantity, p.SellingPrice, request.Products.Select(r => r.Loss).FirstOrDefault())).ToList();

        returnEntity.Update(request.Justification,
                            request.InvoiceNumber,
                            request.UserId,
                            request.ClientId,
                            returnProducts);

        var result = await _returnRepository.UpdateAsync(returnEntity, cancellationToken);
        if (result.IsValid is false)
            return result;

        var commitResult = await _returnRepository.Commit(cancellationToken);
        if (commitResult is false)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        await _stockCalculator.StockCalculateAsync(cancellationToken);
        
        return result;

    }
}