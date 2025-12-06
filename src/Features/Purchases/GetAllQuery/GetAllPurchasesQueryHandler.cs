using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllPurchasesQuery, PagedResponse<GetAllPurchasesQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllPurchasesQueryResponse>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _context.Purchases!.AsNoTracking()
                                                 .Include(p => p.User)
                                                 .Include(p => p.Supplier)
                                                 .Include(P => P.PurchaseProducts)
                                                 .ThenInclude(pp => pp.Product)
                                                 .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = purchases!
            .Select(p => new GetAllPurchasesQueryResponse
            (
                p.PurchaseId,
                p.InvoiceNumber,
                p.PaymentMethod,
                p.TotalPurchase,
                p.CreatedAt,
                p.User.UserName!,
                p.Supplier.CompanyName,
                p.PurchaseProducts.Select(pp => new PurchaseProduct
                (
                    pp.Product.Name,
                    pp.Product.SKU,
                    pp.Quantity,
                    pp.AcquisitionCost,
                    pp.TotalItem
                )).ToList()
            ));

        return new PagedResponse<GetAllPurchasesQueryResponse>(pagedResponse);
    }
}