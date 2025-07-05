using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<GetAllPurchasesQuery, PagedResponse<GetAllPurchasesQueryResponse>>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<PagedResponse<GetAllPurchasesQueryResponse>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _purchaseRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var purchasesResponse = await purchases.Select(p => new GetAllPurchasesQueryResponse(
            p.PurchaseId,
            p.InvoiceNumber,
            p.PaymentMethod,
            p.TotalPurchase,
            p.CreatedAt,
            p.UserId,
            p.SupplierId,
            [.. p.PurchaseProducts.Select(pp => new PurchaseProductDto(
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
        ))
        .ToPagedListAsync(request.PageNumber, request.PageSize);

         return new PagedResponse<GetAllPurchasesQueryResponse>(purchasesResponse);
    }
}