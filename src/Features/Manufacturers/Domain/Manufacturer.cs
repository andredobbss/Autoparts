using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Manufacturers.Domain;

public sealed class Manufacturer
{
    private Manufacturer() { }

    public Guid ManufacturerId { get; private set; }
    public string Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public IReadOnlyCollection<Product> Products { get; private set; } = [];

    public Manufacturer(string description)
    {
        ManufacturerId = Guid.NewGuid();
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;
}
