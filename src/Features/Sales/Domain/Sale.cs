using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Sales.Domain;

public sealed class Sale
{
    private readonly SaleValidator _saleValidator = new();

    private Sale() { }

    public Guid SaleId { get; private set; }
    public string InvoiceNumber { get; private set; } = null!;
    public decimal TotalSale { get; private set; } = 0m;
    public EPaymentMethod PaymentMethod { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;
    public int DaysLastSale => (DateTime.UtcNow - CreatedAt).Days;

    // Foreign Keys
    public Guid UserId { get; private set; }
    public Guid ClientId { get; private set; }

    // Navigation Properties
    public Client Client { get; private set; } = null!;
    public User User { get; private set; } = null!;
    public ICollection<Product> Products { get; private set; } = [];
    public ICollection<SaleProduct> SaleProducts { get; private set; } = [];

    public Sale(Guid saleId, string invoiceNumber, decimal totalSale, EPaymentMethod paymentMethod, Guid userId, Guid clientId, ICollection<SaleProduct> saleProducts)
    {
        SaleId = saleId;
        InvoiceNumber = invoiceNumber;
        TotalSale = totalSale;
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        SaleProducts = saleProducts;

        var validationResult = SaleResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string invoiceNumber, EPaymentMethod paymentMethod, Guid userId, Guid clientId, ICollection<SaleProduct> saleProducts)
    {
        InvoiceNumber = invoiceNumber;
        PaymentMethod = paymentMethod;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        SaleProducts = saleProducts;

        var validationResult = SaleResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void UpdateTotalSale(decimal totalSale) => TotalSale = totalSale;

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult SaleResult()
    {
        return _saleValidator.Validate(this);
    }
}
