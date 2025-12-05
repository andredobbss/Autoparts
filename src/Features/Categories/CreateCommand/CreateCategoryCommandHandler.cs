using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.CreateCommand;

public sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, ValidationResult>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<ValidationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Description);

        var result = await _categoryRepository.AddAsync(category, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _categoryRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}