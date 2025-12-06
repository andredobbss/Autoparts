using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllSalesQuery, PagedResponse<GetAllSalesQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllSalesQueryResponse>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _context.Sales!.AsNoTracking()
                                         .Include(s => s.User)
                                         .Include(s => s.Client)
                                         .Include(s => s.SaleProducts)
                                         .ThenInclude(sp => sp.Product)
                                         .Include(s => s.Products).ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = sales
            .Select(s => new GetAllSalesQueryResponse
            (
                s.SaleId,
                s.InvoiceNumber,
                s.TotalSale,
                s.PaymentMethod,
                s.DaysLastSale,
                s.User.UserName!,
                s.Client.ClientName,
                s.CreatedAt,
                s.SaleProducts.Select(sp => new SaleProduct
                (
                    sp.Product.Name,
                    sp.SKU!,
                    sp.Quantity,
                    sp.SellingPrice,
                    sp.TotalItem
                )).ToList()
            ));

        return new PagedResponse<GetAllSalesQueryResponse>(pagedResponse);
    }
}