using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.ValueObjects;

namespace Autoparts.Api.Features.Suppliers.Domain;

public sealed class Supplier
{
    private Supplier() { }

    public Guid SupplierId { get; private set; }
    public string CompanyName { get; private set; } = null!;
    public string TaxId { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public IReadOnlyCollection<Purchase> Purchases { get; private set; } = [];
    public Address Address { get; private set; } = null!;

    public Supplier(string companyName, DateTime createdAt, Address address)
    {
        SupplierId = Guid.NewGuid();
        CompanyName = companyName;
        CreatedAt = createdAt;
        Address = address;
    }

    public void Update(string companyName, DateTime updatedAt, Address address)
    {
        CompanyName = companyName;
        UpdatedAt = updatedAt;
        Address = address;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

}


