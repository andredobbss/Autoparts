using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, ValidationResult>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<ValidationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            return new ValidationResult([new ValidationFailure(Resource.CATEGORY, string.Format(Resource.CATEGORY_NOT_FOUND, request.CategoryId))]);

        category.Delete();

        var deleted = await _categoryRepository.DeleteAsync(category, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.CATEGORY, Resource.FAILED_TO_DELETE_CATEGORY)]);

        var committed = await _categoryRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}