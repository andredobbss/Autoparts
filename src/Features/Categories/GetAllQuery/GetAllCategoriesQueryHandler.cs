using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoriesQuery, PagedResponse<GetAllCategoriesQueryResponse>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<PagedResponse<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = categories.
                     Select(c => new GetAllCategoriesQueryResponse
                     (
                         c.CategoryId,
                         c.Description,
                         c.CreatedAt
                     ));

        return new PagedResponse<GetAllCategoriesQueryResponse>(pagedResponse);
    }
}