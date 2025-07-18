using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
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

        await _categoryRepository.Commit(cancellationToken);

        return result;
    }

}