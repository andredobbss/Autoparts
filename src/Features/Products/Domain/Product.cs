using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Products.Domain;

public sealed class Product
{
    private readonly ProductValidator _productValidator = new();

    private Product() { }

    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = null!;
    public string TechnicalDescription { get; private set; } = null!;
    public string SKU { get; private set; } = null!;
    public string Compatibility { get; private set; } = null!;
    public decimal AcquisitionCost { get; private set; } = 0m;
    public decimal SellingPrice { get; private set; } = 0m;
    public int Quantity { get; private set; } =0;
    public int Stock { get; private set; } = 0;
    public EStockStatus StockStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public Guid CategoryId { get; private set; }
    public Guid ManufacturerId { get; private set; }

    public Category Category { get; private set; } = null!;
    public Manufacturer Manufacturer { get; private set; } = null!;

    public ICollection<PurchaseProduct> PurchaseProducts { get; private set; } = [];
    public ICollection<ReturnProduct> ReturnProducts { get; private set; } = [];
    public ICollection<SaleProduct> SaleProducts { get; private set; } = [];
    public ICollection<Purchase> Purchases { get; private set; } = [];
    public ICollection<Sale> Sales { get; private set; } = [];
    public ICollection<Return> Returns { get; private set; } = [];

    public Product(
        string name,
        string technicalDescription,
        string compatibility,
        decimal acquisitionCost,     
        string sku,
        Guid categoryId,
        Guid manufacturerId)
    {
        ProductId = Guid.NewGuid();
        Name = name;
        TechnicalDescription = technicalDescription;
        Compatibility = compatibility;
        AcquisitionCost = acquisitionCost;
        SKU = sku;
        SellingPrice = CalculateSellingPrice(acquisitionCost);
        CreatedAt = DateTime.UtcNow;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;

        var validationResult = ProductDomainResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public Product(
        Guid productId,
         string name,
        string technicalDescription,
        string compatibility,
        string sku,
        int quantity, 
        int stock,
        EStockStatus stockStatus, 
        DateTime createdAt,
        decimal acquisitionCost, 
        decimal sellingPrice,
        Guid categoryId,
        Guid manufacturerId)
    {
        ProductId = productId;
        Name = name;
        TechnicalDescription = technicalDescription;
        Compatibility = compatibility;
        SKU = sku;
        Quantity = quantity;
        Stock =stock;
        StockStatus = stockStatus;
        CreatedAt = createdAt;
        AcquisitionCost = acquisitionCost;
        SellingPrice = sellingPrice;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;
    }


    public void Update(
                       string name,
                       string technicalDescription,
                       string compatibility,
                       decimal acquisitionCost,
                       Guid categoryId,
                       Guid manufacturerId)
    {
        Name = name;
        TechnicalDescription = technicalDescription;
        Compatibility = compatibility;
        AcquisitionCost = acquisitionCost;
        SellingPrice = CalculateSellingPrice(acquisitionCost);
        UpdatedAt = DateTime.UtcNow;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;

        var validationResult = ProductDomainResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

   
    public void SetStock(int stock = 0)
    {
        if (stock < 0)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);

        Stock = stock;

        StockStatus = Stock switch
        {

            0 => EStockStatus.None,
            < 3 => EStockStatus.Backordered,
            _ => EStockStatus.Available
        };
    }

    private decimal CalculateSellingPrice(decimal acquisitionCost)
    {
        if (acquisitionCost <= 0)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ProductDomainResult().Errors);

        return acquisitionCost * Constants.ProfitMargin; 
    }

    private ValidationResult ProductDomainResult()
    {
        return _productValidator.Validate(this);
    }


}
