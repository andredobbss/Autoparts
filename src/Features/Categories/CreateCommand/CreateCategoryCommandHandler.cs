using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using MediatR;
namespace Autoparts.Api.Features.Categories.CreateCommand;
public sealed class CreateCategoryCommandHandler(CategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.description);

        var result = await _categoryRepository.AddAsync(category, cancellationToken);

        if (result.IsValid is true)
            return category;

        throw new ArgumentException($"Invalid description: {result.ToDictionary()}");

        //return category.CategpryResult().Errors.FirstOrDefault() switch
        //{
        //    var error when error.ErrorMessage.Contains("Description") => throw new ArgumentException($"Invalid description: {error.ErrorMessage}"),
        //    _ => throw new Exception("An error occurred while creating the category.")
        //};
    }

}