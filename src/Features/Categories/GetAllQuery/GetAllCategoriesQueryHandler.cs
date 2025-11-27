using AutoMapper;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, PagedResponse<GetAllCategoriesQueryResponse>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var categoriesResponse = _mapper.Map<IEnumerable<GetAllCategoriesQueryResponse>>(categories.ToList());

        var pagedResponse = new StaticPagedList<GetAllCategoriesQueryResponse>(categoriesResponse, categories.GetMetaData());

        return new PagedResponse<GetAllCategoriesQueryResponse>(pagedResponse);
    }
}