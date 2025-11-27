using AutoMapper;
using Autoparts.Api.Features.Categories.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        return _mapper.Map<GetCategoryByIdQueryResponse>(category);
    }
}