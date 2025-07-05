using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Products.Stock;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.CreateCommand;
public sealed class CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IStockCalculator stockCalculator, IProductList productList) : IRequestHandler<CreatePurchaseCommand, ValidationResult>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        Guid purchaseId = Guid.NewGuid();

        List<PurchaseProduct> purchaseProducts = [];

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);

        foreach (var product in productsList)
        {
            var purchaseProduct = new PurchaseProduct(purchaseId, product.ProductId, product.Quantity, product.AcquisitionCost);

            purchaseProducts.Add(purchaseProduct);
        }

        var purchase = new Purchase(purchaseId, request.InvoiceNumber, 0m, request.PaymentMethod, request.UserId, request.SupplierId, purchaseProducts);

        await _purchaseRepository.AddAsync(purchase, cancellationToken);

        var resultCalculation = await _stockCalculator.AddCalculateStockAsync(productsList, cancellationToken);
        if (resultCalculation.IsValid is false)
            return resultCalculation;

        var totalPurchase = await _purchaseRepository.GetTotalPurchase(purchaseId, cancellationToken);

        purchase.UpdateTotalPurchase(totalPurchase);

        var result = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        if (result.IsValid is false)
            return result;

        var commitResult = await _purchaseRepository.Commit(cancellationToken);
        if (commitResult is false)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return result;
    }
}