using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;

namespace Autoparts.Api.Features.Sales.Domain;

public sealed class Sale
{ 
    private Sale() { }

    public Guid SaleId { get; private set; }
    public string InvoiceNumber { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public Guid UserId { get; private set; } = Guid.Empty; //ok
    public Guid ClientId { get; private set; } //ok

    public Client Client { get; private set; } = null!; //ok
    public User User { get; private set; } = null!; //ok  
    public ICollection<Product> Products { get; private set; } = [];
    public ICollection<SaleProduct> SaleProducts { get; private set; } = [];
    public Sale(string invoiceNumber, Guid userId, Guid clientId)
    {
        SaleId = Guid.NewGuid();
        InvoiceNumber = invoiceNumber;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Update(string invoiceNumber, Guid userId, Guid clientId)
    {
        InvoiceNumber = invoiceNumber;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;


}
