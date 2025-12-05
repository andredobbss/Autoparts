using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.CreateCommand;

public sealed class CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IProductList productList) : IRequestHandler<CreatePurchaseCommand, ValidationResult>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IProductList _productList = productList;
    public async Task<ValidationResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(nameof(request.Products), Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products, cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(nameof(productsList), Resource.PRODUCTS_NOT_FOUND)]);

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

        var result = await _purchaseRepository.AddAsync(purchase, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _purchaseRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }

}

