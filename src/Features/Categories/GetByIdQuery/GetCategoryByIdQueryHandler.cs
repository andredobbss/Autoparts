using Autoparts.Api.Features.Categories.Infraestructure;
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
                         category.CreatedAt);
    }
}