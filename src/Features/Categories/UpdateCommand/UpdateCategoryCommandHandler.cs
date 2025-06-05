using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Categories.UpdateCommand;
public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly CategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.id, cancellationToken) ?? 
            throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} {request.id}");

        category.Update(request.description);

        var result = await _categoryRepository.UpdateAsync(category, cancellationToken);

        if (result.IsValid is true)
            return category;

        throw new ArgumentException($"Invalid description: {result.ToDictionary()}");

        //return result.Errors.FirstOrDefault() switch
        //{
        //    var error when error.ErrorMessage.Contains("Description") => throw new ArgumentException($"Invalid description: {error.ErrorMessage}"),
        //    _ => throw new Exception("An error occurred while updating the category.")
        //};
    }
}