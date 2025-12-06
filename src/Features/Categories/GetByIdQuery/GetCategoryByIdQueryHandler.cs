using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.DTOs;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories!.Include(c => c.Products)
                                         .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
        if (category is null)
            throw new KeyNotFoundException(string.Format(Resource.CATEGORY_NOT_FOUND, request.CategoryId));

        return new GetCategoryByIdQueryResponse(
                         category.CategoryId,
                         category.Description,
                         category.CreatedAt,
                         category.Products.Select(p => new ProductDto
                         (
                               p.ProductId,
                               p.Name,
                               p.TechnicalDescription,
                               p.SKU,
                               p.Compatibility,
                               p.AcquisitionCost,
                               p.SellingPrice
                         )).ToList()
                        );
    }
}