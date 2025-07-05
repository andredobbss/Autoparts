using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoriesQuery, PagedResponse<Category>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<PagedResponse<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new PagedResponse<Category>(categories);
    }
}