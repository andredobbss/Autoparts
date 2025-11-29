using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Sales.CreateCommand;

public sealed class CreateSaleCommandHandler(ISaleRepository saleRepository, IProductList productList) : IRequestHandler<CreateSaleCommand, ValidationResult>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(nameof(request.Products), Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products.Select(r => new SharedProductsDto(request.Products.Select(r => r.ProductId).First(), request.Products.Select(r => r.Quantity).First())), cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(nameof(productsList), Resource.PRODUCTS_NOT_FOUND)]);

        foreach (var product in productsList)
        {
            var requestedProduct = request.Products.FirstOrDefault(rp => rp.ProductId == product.ProductId);

            if (requestedProduct is null || requestedProduct.Quantity <= 0)
                return new ValidationResult([new ValidationFailure(nameof(request.Products), Resource.PRODUCTS_NOT_FOUND)]);

            if (product.StockStatus == EStockStatus.None)
                return new ValidationResult([new ValidationFailure(nameof(product.StockStatus), Resource.STOCK_ZERO)]);

            if (product.Stock < requestedProduct.Quantity)
                return new ValidationResult([new ValidationFailure(nameof(product.Stock), Resource.STOCK_INSUFFICIENT)]);
        }

        Guid saleId = Guid.NewGuid();

        var saleProducts = productsList
            .Select(product => new SaleProduct(saleId, product.ProductId, product.Quantity, product.SellingPrice))
            .ToList();
        if (saleProducts is null || !saleProducts.Any())
            return new ValidationResult([new ValidationFailure(nameof(saleProducts), Resource.PRODUCTS_NOT_FOUND)]);

        var totalSale = saleProducts.Sum(sp => sp.TotalItem);

        var sale = new Sale(
            saleId,
            request.InvoiceNumber,
            totalSale,
            request.PaymentMethod,
            request.UserId,
            request.ClientId,
            saleProducts
        );

        var result = await _saleRepository.AddAsync(sale, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _saleRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}