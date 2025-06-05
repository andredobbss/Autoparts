using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Categories.Domain;

public sealed class Category
{
    private readonly CategoryValidator _categoryValidation = new();
    public Category() { }

    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public IReadOnlyCollection<Product> Products { get; private set; } = [];

    public Category(string description)
    {
        CategoryId = Guid.NewGuid();
        Description = description;
        CreatedAt = DateTime.UtcNow;

        if (!CategpryResult().IsValid)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, CategpryResult().Errors);
    }

    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    public ValidationResult CategpryResult()
    {
        return _categoryValidation.Validate(this);
    }

}
