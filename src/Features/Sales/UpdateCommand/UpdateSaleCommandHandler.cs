using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Sales.UpdateCommand;

public sealed class UpdateSaleCommandHandler(ISaleRepository saleRepository, IProductList productList) : IRequestHandler<UpdateSaleCommand, ValidationResult>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    private readonly IProductList _productList = productList;

    public async Task<ValidationResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

        foreach (var product in productsList)
        {
            var requestedProduct = request.Products.FirstOrDefault(rp => rp.ProductId == product.ProductId);

            if (requestedProduct is null || requestedProduct.Quantity <= 0)
                return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

            if (product.StockStatus == EStockStatus.None)
                return new ValidationResult([new ValidationFailure(Resource.STOCK, Resource.STOCK_ZERO)]);

            if (product.Stock < requestedProduct.Quantity)
                return new ValidationResult([new ValidationFailure(Resource.STOCK, Resource.STOCK_INSUFFICIENT)]);
        }

        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale is null)
            return new ValidationResult([new ValidationFailure(Resource.SALE, string.Format(Resource.SALES_NOT_FOUND, request.SaleId))]);

        var saleProducts = productsList.Select(product => new SaleProduct(request.SaleId, product.ProductId, product.Quantity, product.SellingPrice)).ToList();

        sale.Update(
            request.InvoiceNumber,
            request.PaymentMethod,
            request.UserId,
            request.ClientId,
            saleProducts);

        var totalSale = saleProducts.Sum(sp => sp.TotalItem);

        sale.UpdateTotalSale(totalSale);

        var result = await _saleRepository.UpdateAsync(sale, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _saleRepository.CommitAsync(cancellationToken);
        if (!commitResult)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return result;
    }
}