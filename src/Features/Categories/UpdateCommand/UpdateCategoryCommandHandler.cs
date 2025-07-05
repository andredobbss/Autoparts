using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.UpdateCommand;
public sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, ValidationResult>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<ValidationResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            return new ValidationResult { Errors = { new ValidationFailure("Category", $"{Resource.ID_NOT_FOUND} : {request.CategoryId}") } };

        category.Update(request.Description);

        var result = await _categoryRepository.UpdateAsync(category, cancellationToken);

        return result;
    }
}