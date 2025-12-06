using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Sales.CreateCommand;

public sealed class CreateSaleCommandHandler(AutopartsDbContext context, IProductList productList, IStockCalculator stockCalculator) : IRequestHandler<CreateSaleCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    private readonly IProductList _productList = productList;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
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

        Guid saleId = Guid.NewGuid();

        var saleProducts = productsList.Select(product => new SaleProduct(saleId, product.ProductId, product.Quantity, product.SellingPrice)).ToList(); 

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

        await _context.Sales!.AddAsync(sale, cancellationToken);

        #region Transaction and Commit

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await _context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await _stockCalculator.StockCalculateAsync(cancellationToken);

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