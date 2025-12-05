using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Purchases.Domain;

public sealed class Purchase
{
    private readonly PurchaseValidator _purchaseValidator = new();

    private Purchase() { }

    public Guid PurchaseId { get; private set; }
    public string InvoiceNumber { get; private set; } = null!;
    public decimal TotalPurchase { get; private set; } = 0m;
    public EPaymentMethod PaymentMethod { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    // Foreign Keys
    public Guid UserId { get; private set; }
    public Guid SupplierId { get; private set; }

    // Navigation Properties
    public User User { get; private set; } = null!;
    public Supplier Supplier { get; private set; } = null!;
    public ICollection<PurchaseProduct> PurchaseProducts { get; private set; } = [];
    public ICollection<Product> Products { get; private set; } = [];

    public Purchase(Guid purchaseId, string invoiceNumber, decimal totalPurchase, EPaymentMethod paymentMethod, Guid userId, Guid supplierId, ICollection<PurchaseProduct> purchaseProducts)
    {
        PurchaseId = purchaseId;
        InvoiceNumber = invoiceNumber;
        TotalPurchase = totalPurchase;
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        SupplierId = supplierId;
        PurchaseProducts = purchaseProducts;

        var validationResult = PurchaseResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string invoiceNumber, EPaymentMethod paymentMethod, Guid userId, Guid supplierId, ICollection<PurchaseProduct> purchaseProducts)
    {
        InvoiceNumber = invoiceNumber;
        PaymentMethod = paymentMethod;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        SupplierId = supplierId;
        PurchaseProducts = purchaseProducts;

        var validationResult = PurchaseResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void UpdateTotalPurchase(decimal totalPurchase)
    {
        TotalPurchase = totalPurchase;

        var validationResult = PurchaseResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult PurchaseResult()
    {
        return _purchaseValidator.Validate(this);
    }
}