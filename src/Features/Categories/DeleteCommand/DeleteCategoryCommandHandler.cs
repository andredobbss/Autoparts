using Autoparts.Api.Features.Categories.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Categories.DeleteCommand;
public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            return false;

        category.Delete();

        var deleted = await _categoryRepository.DeleteAsync(category, cancellationToken);
        if (!deleted)
            return false;

        var committed = await _categoryRepository.CommitAsync(cancellationToken);
        return committed;
    }
}