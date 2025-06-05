using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Products.Domain;

public sealed class Product
{
    private readonly ProductValidator _productValidator = new();

    private Product() { }

    public Guid ProductId { get; private set; }
    public string TechnicalDescription { get; private set; } = null!;
    public string SKU { get; private set; } = null!;
    public string Compatibility { get; private set; } = null!;
    public decimal AcquisitionCost { get; private set; } = 0m;
    public decimal SellingPrice { get; private set; } = 0m;
    public uint Stock { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public Guid CategoryId { get; private set; }
    public Guid ManufacturerId { get; private set; }

    public Category Category { get; private set; } = null!;
    public Manufacturer Manufacturer { get; private set; } = null!;
    public IReadOnlyCollection<Purchase> Purchases { get; private set; } = [];
    public IReadOnlyCollection<Sale> Sales { get; private set; } = [];
    public IReadOnlyCollection<Return> Returns { get; private set; } = [];

    internal Product(
        string technicalDescription,
        string compatibility,
        decimal acquisitionCost,
        Guid categoryId,
        Guid manufacturerId)
    {
        ProductId = Guid.NewGuid();
        TechnicalDescription = technicalDescription;
        Compatibility = compatibility;
        AcquisitionCost = acquisitionCost;
        SellingPrice = CalculateSellingPrice(acquisitionCost);
        CreatedAt = DateTime.UtcNow;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;

        if (!ProductDomainResult().IsValid)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);
    }

    internal void Update(string technicalDescription,
                       string compatibility,
                       decimal acquisitionCost,
                       Guid categoryId,
                       Guid manufacturerId)
    {
        TechnicalDescription = technicalDescription;
        Compatibility = compatibility;
        AcquisitionCost = acquisitionCost;
        SellingPrice = CalculateSellingPrice(acquisitionCost);
        UpdatedAt = DateTime.UtcNow;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;

        if (!ProductDomainResult().IsValid)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);
    }

    internal void Delete() => DeletedAt = DateTime.UtcNow;

    internal void SetSku(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);

        SKU = sku;
    }

    private decimal CalculateSellingPrice(decimal acquisitionCost)
    {
        if (acquisitionCost <= 0)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);

        return acquisitionCost * Constants.ProfitMargin; 
    }

    public ValidationResult ProductDomainResult()
    {
        return _productValidator.Validate(this);
    }


}
