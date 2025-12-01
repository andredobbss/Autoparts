using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQueryHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<GetPurchaseByIdQuery, GetPurchaseByIdQueryResponse>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<GetPurchaseByIdQueryResponse> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase == null)
            throw new KeyNotFoundException(string.Format(Resource.PURCHASE_NOT_FOUND, request.PurchaseId));

        return new GetPurchaseByIdQueryResponse
            (
                purchase!.PurchaseId,
                purchase.InvoiceNumber,
                purchase.PaymentMethod,
                purchase.TotalPurchase,
                purchase.CreatedAt,
                purchase.User.UserName,
                purchase.Supplier.CompanyName,
                purchase.Products.Select(pp => new ProductDto
                (
                    pp.ProductId,
                    pp.Name,
                    pp.TechnicalDescription,
                    pp.SKU,
                    pp.Compatibility,
                    pp.AcquisitionCost,
                    pp.SellingPrice
                )).ToList()
            );
    }
}