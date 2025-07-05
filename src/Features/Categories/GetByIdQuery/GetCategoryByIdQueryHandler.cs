using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
    }
}