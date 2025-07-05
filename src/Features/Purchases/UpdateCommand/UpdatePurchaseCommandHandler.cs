using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Products.Stock;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.UpdateCommand;
public sealed class UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IStockCalculator stockCalculator, IProductList productList) : IRequestHandler<UpdatePurchaseCommand, ValidationResult>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return new ValidationResult { Errors = { new ValidationFailure("Purchase", $"{Resource.ID_NOT_FOUND} : {request.PurchaseId}") } };

        List<PurchaseProduct> purchaseProducts = [];

        var purchaseProductList = purchase.PurchaseProducts
            .Where(pp => pp.PurchaseId == request.PurchaseId)
            .Select(pp => new { pp.ProductId, pp.Quantity })
            .ToList();

        IDictionary<Guid, int> idAndQuantityDictionary = new Dictionary<Guid, int>();

        foreach (var purchaseProduct in purchaseProductList)
        {
            idAndQuantityDictionary.Add(purchaseProduct.ProductId, purchaseProduct.Quantity);
        }

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);

        var resultCalculationSubtraction = await _stockCalculator.UpdateCalculateStockAsync(productsList, idAndQuantityDictionary, request.PurchaseId, cancellationToken);
        if (resultCalculationSubtraction.IsValid is false)
            return resultCalculationSubtraction;

        foreach (var product in productsList)
        {
            var purchaseProduct = new PurchaseProduct(request.PurchaseId, product.ProductId, product.Quantity, product.AcquisitionCost);

            purchaseProducts.Add(purchaseProduct);
        }

        var totalPurchase = purchaseProducts.Sum(p => p.TotalItem);

        purchase.UpdateTotalPurchase(totalPurchase);

        purchase.Update(request.InvoiceNumber,
                        request.PaymentMethod,
                        request.UserId,
                        request.SupplierId,
                        purchaseProducts);

        var result = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        if (result.IsValid is false)
            return result;

        var commitResult = await _purchaseRepository.Commit(cancellationToken);
        if (commitResult is false)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return result;
    }
}