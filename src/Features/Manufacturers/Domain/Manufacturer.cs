using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Manufacturers.Domain;

public sealed class Manufacturer
{
    private readonly ManufacturerValidator _manufacturerValidation = new();
    private Manufacturer() { }

    public Guid ManufacturerId { get; private set; }
    public string Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public ICollection<Product> Products { get; private set; } = [];

    public Manufacturer(string description)
    {
        ManufacturerId = Guid.NewGuid();
        Description = description;
        CreatedAt = DateTime.UtcNow;

        if (ManufacturerResult().IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ManufacturerResult().Errors);
    }

    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;

        if (ManufacturerResult().IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ManufacturerResult().Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ManufacturerResult()
    {
        return _manufacturerValidation.Validate(this);
    }
}
