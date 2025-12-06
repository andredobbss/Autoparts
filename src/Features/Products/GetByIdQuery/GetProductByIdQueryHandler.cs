using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products!.Include(p => p.Category)
                                              .Include(p => p.Manufacturer)
                                              .Include(p => p.Sales)
                                              .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
        if (product is null)
            throw new KeyNotFoundException(string.Format(Resource.PRODUCTS_NOT_FOUND, request.ProductId));

        return
           new GetProductByIdQueryResponse
           (
               product!.ProductId,
               product.Name,
               product.TechnicalDescription,
               product.SKU,
               product.Compatibility,
               product.AcquisitionCost,
               product.SellingPrice,
               product.Stock,
               product.StockStatus,
               product.Sales.Select(s => (DateTime.UtcNow - s.CreatedAt).Days > 90 ? EStockStatusOverTime.StagnantStock : EStockStatusOverTime.Active).FirstOrDefault(),
               product.Category.Description,
               product.Manufacturer.Description,
               product.CreatedAt);
    }
}