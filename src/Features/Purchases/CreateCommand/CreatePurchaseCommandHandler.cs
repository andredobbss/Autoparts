using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.CreateCommand;

public sealed class CreatePurchaseCommandHandler(AutopartsDbContext context, IProductList productList, IStockCalculator stockCalculator) : IRequestHandler<CreatePurchaseCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    private readonly IProductList _productList = productList;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

        Guid purchaseId = Guid.NewGuid();

        var purchaseProducts = productsList.Select(product => new PurchaseProduct(purchaseId, product.ProductId, product.Quantity, product.AcquisitionCost)).ToList();

        var totalPurchase = purchaseProducts.Sum(p => p.TotalItem);

        var purchase = new Purchase(
            purchaseId,
            request.InvoiceNumber,
            totalPurchase,
            request.PaymentMethod,
            request.UserId,
            request.SupplierId,
            purchaseProducts
        );

        await _context.AddAsync(purchase, cancellationToken);

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

