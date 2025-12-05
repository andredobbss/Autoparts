using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Products.DTOs;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
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