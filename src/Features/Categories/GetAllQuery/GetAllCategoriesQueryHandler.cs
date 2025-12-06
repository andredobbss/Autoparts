using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using Autoparts.Api.Shared.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllCategoriesQuery, PagedResponse<GetAllCategoriesQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories!.AsNoTracking()
                                         .Include(c => c.Products)
                                         .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = categories.
                     Select(c => new GetAllCategoriesQueryResponse
                     (
                         c.CategoryId,
                         c.Description,
                         c.CreatedAt,
                         c.Products.Select(p => new ProductDto
                         (
                               p.ProductId,
                               p.Name,
                               p.TechnicalDescription,
                               p.SKU,
                               p.Compatibility,
                               p.AcquisitionCost,
                               p.SellingPrice
                         )).ToList()
                     ));

        return new PagedResponse<GetAllCategoriesQueryResponse>(pagedResponse);
    }
}