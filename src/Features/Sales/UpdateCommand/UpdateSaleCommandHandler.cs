using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Sales.UpdateCommand;

public sealed class UpdateSaleCommandHandler(AutopartsDbContext context, IProductList productList, IStockCalculator stockCalculator) : IRequestHandler<UpdateSaleCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await productList.GetProductsListAsync(request.Products, cancellationToken);
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

        var sale = await context.Sales!.FindAsync(request.SaleId, cancellationToken);
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

         context.Sales!.Update(sale);

        #region Transaction and Commit

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await stockCalculator.StockCalculateAsync(cancellationToken);

            if (commitResult > 0 && stockUpdated)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ValidationResult();
            }

            await transaction.RollbackAsync(cancellationToken);
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        #endregion
    }
}