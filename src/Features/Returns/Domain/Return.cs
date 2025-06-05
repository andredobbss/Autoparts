using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;

namespace Autoparts.Api.Features.Returns.Domain;

public sealed class Return
{
    private Return() { }
 
    public Guid ReturnId { get; private set; }
    public string Justification { get; private set; } = string.Empty;
    public string InvoiceNumber { get; private set; } = string.Empty;
    public uint Quantity { get; private set; } = 0;
    public bool Loss { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public string UserId { get; private set; } = null!; //ok
    public Guid ClientId { get; private set; } // ok

    public User User { get; private set; } = null!; //ok
    public Client Client { get; private set; } = null!; // ok
    public IReadOnlyCollection<Product> Products { get; private set; } = []; //ok

    public Return(string justification, string invoiceNumber, uint quantity, bool loss, string userId, Guid clientId)
    {
        ReturnId = Guid.NewGuid();
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Quantity = quantity;
        Loss = loss;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Update(string justification, string invoiceNumber, uint quantity, bool loss, string userId, Guid clientId)
    {
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Quantity = quantity;
        Loss = loss;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

}
