using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using Autoparts.Api.Shared.Products.DTOs;
using NetDevPack.SimpleMediator;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryHandler(AutopartsDbContext _context) : IRequestHandler<GetAllManufacturersQuery, PagedResponse<GetAllManufacturersQueryResponse>>
{
    public async Task<PagedResponse<GetAllManufacturersQueryResponse>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        var manufactures = await _context.Manufacturers!.AsNoTracking()
                                            .Include(m => m.Products)
                                            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = manufactures
            .Select(m => new GetAllManufacturersQueryResponse
            (
                m.ManufacturerId,
                m.Description,
                m.CreatedAt,
                m.Products.Select(pp => new ProductDto
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

        return new PagedResponse<GetAllManufacturersQueryResponse>(pagedResponse);
    }
}