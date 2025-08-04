using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;
public sealed class CreateReturnCommandHandler(IReturnRepository returnRepository, IStockCalculator stockCalculator, IProductList productList) : IRequestHandler<CreateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(CreateReturnCommand request, CancellationToken cancellationToken)
    {
        Guid returnId = Guid.NewGuid();

        var productsList = await _productList.GetProductsListAsync(request.Products.Select(r => new SharedProductsDto(request.Products.Select(r => r.ProductId).First(), request.Products.Select(r => r.Quantity).First())), cancellationToken);

        var returnProducts = productsList.Select(p => new ReturnProduct(returnId, p.ProductId, p.Quantity, p.SellingPrice, request.Products.Select(r => r.Loss).FirstOrDefault())).ToList();

        var returnEntity = new Return(returnId, request.Justification, request.InvoiceNumber, request.UserId, request.ClientId, returnProducts);

        await _returnRepository.AddAsync(returnEntity, cancellationToken);

        var commitResult = await _returnRepository.Commit(cancellationToken);

        var result = await _stockCalculator.StockCalculateAsync(cancellationToken);
        if (result.IsValid is false)
            return result;

        return result;


    }
}