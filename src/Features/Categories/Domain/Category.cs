using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Categories.Domain;

public sealed class Category
{
    private readonly CategoryValidator _categoryValidation = new();
    private Category() { }

    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public ICollection<Product> Products { get; private set; } = [];

    public Category(string description)
    {
        CategoryId = Guid.NewGuid();
        Description = description;
        CreatedAt = DateTime.UtcNow;

        var validationResult = CategoryResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;

        var validationResult = CategoryResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult CategoryResult()
    {
        return _categoryValidation.Validate(this);
    }

}
