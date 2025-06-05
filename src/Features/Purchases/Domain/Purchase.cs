using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Users.Domain;

namespace Autoparts.Api.Features.Purchases.Domain;

public sealed class Purchase
{
    private Purchase() { }

    public Guid PurchaseId { get; private set; }
    public string InvoiceNumber { get; private set; } = null!;
    public uint Quantity { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public string UserId { get; private set; } = null!;
    public Guid SupplierId { get; private set; } //ok
    //public Guid ProductId { get; private set; }
    public User User { get; private set; } = null!;
    public Supplier Supplier { get; private set; } = null!; //ok

    public IReadOnlyCollection<Product> Products { get; private set; } = []; //ok


    public Purchase(string invoiceNumber, string userId)
    {
        PurchaseId = Guid.NewGuid();
        InvoiceNumber = invoiceNumber;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
    }

    public void Update(string invoiceNumber, string userId)
    {
        InvoiceNumber = invoiceNumber;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;


}
