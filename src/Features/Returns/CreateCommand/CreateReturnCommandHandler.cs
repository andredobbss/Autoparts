using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
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

        List<ReturnProduct> returnProducts = [];

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);

        foreach (var product in productsList)
        {
            var returnProduct = new ReturnProduct(returnId, product.ProductId, product.Quantity, product.SellingPrice);
            returnProducts.Add(returnProduct);
        }

        var returnEntity = new Return(returnId, request.Justification, request.InvoiceNumber, request.Loss, request.UserId, request.ClientId, returnProducts);
       
        await _returnRepository.AddAsync(returnEntity, cancellationToken);

        var commitResult = await _returnRepository.Commit(cancellationToken);

        var result = await _stockCalculator.StockCalculateAsync(cancellationToken);
        if (result.IsValid is false)
            return result;

        return result;


    }
}