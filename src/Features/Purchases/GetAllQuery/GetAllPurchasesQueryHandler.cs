using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using Autoparts.Api.Shared.Products.Dto;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;
public sealed record GetAllPurchasesQueryHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<GetAllPurchasesQuery, PagedResponse<GetAllPurchasesQueryResponse>>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<PagedResponse<GetAllPurchasesQueryResponse>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _purchaseRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = purchases!
            .Select(p => new GetAllPurchasesQueryResponse
            (
                p.PurchaseId,
                p.InvoiceNumber,
                p.PaymentMethod,
                p.TotalPurchase,
                p.CreatedAt,
                p.User.UserName,
                p.Supplier.CompanyName,
                p.Products.Select(pp => new ProductDto
                (
                    pp.ProductId,
                    pp.Name,
                    pp.TechnicalDescription,
                    pp.SKU,
                    pp.Compatibility,
                    pp.AcquisitionCost,
                    pp.SellingPrice
                )).ToList()
            ));

        return new PagedResponse<GetAllPurchasesQueryResponse>(pagedResponse);
    }
}