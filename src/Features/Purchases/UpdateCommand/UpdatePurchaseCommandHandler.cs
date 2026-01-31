using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Purchases.UpdateCommand;

public sealed class UpdatePurchaseCommandHandler(AutopartsDbContext context, IProductList productList, IStockCalculator stockCalculator) : IRequestHandler<UpdatePurchaseCommand, ValidationResult>
{
    private readonly AutopartsDbContext context = context;
    private readonly IProductList productList = productList;
    private readonly IStockCalculator stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

        var purchase = await context.Purchases!.FindAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return new ValidationResult([new ValidationFailure(Resource.PURCHASE, string.Format(Resource.PURCHASE_NOT_FOUND, request.PurchaseId))]);

        var purchaseProducts = productsList.Select(product => new PurchaseProduct(request.PurchaseId, product.ProductId, product.Quantity, product.AcquisitionCost)).ToList();

        purchase.Update(request.InvoiceNumber,
                        request.PaymentMethod,
                        request.UserId,
                        request.SupplierId,
                        purchaseProducts);

        var totalPurchase = purchaseProducts.Sum(p => p.TotalItem);

        purchase.UpdateTotalPurchase(totalPurchase);

        context.Purchases!.Update(purchase);

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