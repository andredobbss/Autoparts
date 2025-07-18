using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Suppliers.Domain;

public sealed class Supplier
{
    private readonly SupplierValidator _supplierValidator = new();
    private Supplier() { }

    public Guid SupplierId { get; private set; }
    public string CompanyName { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public ICollection<Purchase> Purchases { get; private set; } = [];
    public Address Address { get; private set; } = null!;

    public Supplier(string companyName, Address address)
    {
        SupplierId = Guid.NewGuid();
        CompanyName = companyName;
        CreatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = SupplierResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string companyName, Address address)
    {
        CompanyName = companyName;
        UpdatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = SupplierResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult SupplierResult()
    {
        return _supplierValidator.Validate(this);
    }

}


