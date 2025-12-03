using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Suppliers.Domain;

public sealed class Supplier
{
    private readonly SupplierValidator _supplierValidator = new();
    private Supplier() { }

    public Guid SupplierId { get; private set; }
    public string CompanyName { get; private set; }
    public string? Email { get; private set; }
    public ETaxIdType? TaxIdType { get; private set; }
    public string? TaxId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public ICollection<Purchase> Purchases { get; private set; } = [];

    public Address Address { get; private set; }

    public Supplier(string companyName,
                    Address address,
                    string? email = null,
                    ETaxIdType? taxIdType = null,
                    string? taxId = null)
    {
        SupplierId = Guid.NewGuid();
        CompanyName = companyName;
        TaxIdType = taxIdType;
        Email = email;
        TaxId = taxId;
        CreatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = SupplierResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string companyName,
                       Address address,
                       string? email = null,
                       ETaxIdType? taxIdType = null,
                       string? taxId = null)
    {
        CompanyName = companyName;
        TaxIdType = taxIdType;
        Email = email;
        TaxId = taxId;
        UpdatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = SupplierResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult SupplierResult()
    {
        return _supplierValidator.Validate(this);
    }

}


