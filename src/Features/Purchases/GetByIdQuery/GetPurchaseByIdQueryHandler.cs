using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Features.Purchases.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQueryHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<GetPurchaseByIdQuery, GetPurchaseByIdQueryResponse>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<GetPurchaseByIdQueryResponse> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);

        var purchasesResponse = new GetPurchaseByIdQueryResponse(
           purchase.PurchaseId,
           purchase.InvoiceNumber,
           purchase.PaymentMethod,
           purchase.TotalPurchase,
           purchase.CreatedAt,
           purchase.UserId,
           purchase.SupplierId,
           [.. purchase.PurchaseProducts.Select(pp => new PurchaseProductDto(
               pp.ProductId,
               pp.Quantity,
               pp.AcquisitionCost,
               pp.TotalItem,
               new ProductDto(
                    pp.Product.ProductId,
                    pp.Product.Name,
                    pp.Product.TechnicalDescription,
                    pp.Product.SKU,
                    pp.Product.Compatibility,
                    pp.Product.AcquisitionCost,
                    pp.Product.SellingPrice,
                    pp.Product.Stock,
                    pp.Product.StockStatus,
                    pp.Product.CategoryId,
                    pp.Product.ManufacturerId))
           )]
       );

        return purchasesResponse;
    }
}