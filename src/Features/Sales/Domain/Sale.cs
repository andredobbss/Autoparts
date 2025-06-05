using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;

namespace Autoparts.Api.Features.Sales.Domain;

public sealed class Sale
{ 
    private Sale() { }

    public Guid SaleId { get; private set; }
    public string InvoiceNumber { get; private set; } = null!;
    public uint Quantity { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public string UserId { get; private set; } = null!; //ok
    public Guid ClientId { get; private set; } //ok

    public Client Client { get; private set; } = null!; //ok
    public User User { get; private set; } = null!; //ok  
    public IReadOnlyCollection<Product> Products { get; private set; } = []; //ok

    public Sale(string invoiceNumber, uint quantity, string userId, Guid clientId)
    {
        SaleId = Guid.NewGuid();
        InvoiceNumber = invoiceNumber;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Update(string invoiceNumber, uint quantity, string userId, Guid clientId)
    {
        InvoiceNumber = invoiceNumber;
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;


}
