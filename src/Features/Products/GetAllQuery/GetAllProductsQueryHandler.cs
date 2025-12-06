using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllProductsQuery, PagedResponse<GetAllProductsQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllProductsQueryResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products!.AsNoTracking()
                                               .Include(p => p.Category)
                                               .Include(p => p.Manufacturer)
                                               .Include(p => p.Sales)
                                               .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        var pagedResponse = products.
                     Select(p => new GetAllProductsQueryResponse
                     (
                         p.ProductId,
                         p.Name,
                         p.TechnicalDescription,
                         p.SKU,
                         p.Compatibility,
                         p.AcquisitionCost,
                         p.SellingPrice,
                         p.Stock,
                         p.StockStatus,
                         p.Sales.Select(s =>  (DateTime.UtcNow - s.CreatedAt).Days > 90? EStockStatusOverTime.StagnantStock : EStockStatusOverTime.Active).FirstOrDefault(),
                         p.Category.Description,
                         p.Manufacturer.Description,
                         p.CreatedAt));

        return new PagedResponse<GetAllProductsQueryResponse>(pagedResponse);
    }
}