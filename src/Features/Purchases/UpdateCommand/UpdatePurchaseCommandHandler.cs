using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.UpdateCommand;

public sealed class UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IProductList productList) : IRequestHandler<UpdatePurchaseCommand, ValidationResult>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(nameof(request.Products), Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(nameof(productsList), Resource.PRODUCTS_NOT_FOUND)]);

        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return new ValidationResult { Errors = { new ValidationFailure("Purchase", $"{Resource.ID_NOT_FOUND} : {request.PurchaseId}") } };

        var purchaseProducts = productsList.Select(product => new PurchaseProduct(request.PurchaseId, product.ProductId, product.Quantity, product.AcquisitionCost)).ToList();
        if (purchaseProducts is null || purchaseProducts.Any())
            return new ValidationResult([new ValidationFailure(nameof(purchaseProducts), Resource.PURCHASE_NOT_FOUND)]);

        purchase.Update(request.InvoiceNumber,
                        request.PaymentMethod,
                        request.UserId,
                        request.SupplierId,
                        purchaseProducts);

        var totalPurchase = purchaseProducts.Sum(p => p.TotalItem);

        purchase.UpdateTotalPurchase(totalPurchase);

        var result = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _purchaseRepository.CommitAsync(cancellationToken);
        if (!commitResult)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return result;
    }
}