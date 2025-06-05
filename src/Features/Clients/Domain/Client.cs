using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.ValueObjects;

namespace Autoparts.Api.Features.Clients.Domain;

public sealed class Client
{
    private Client() { }

    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = null!;
    public string TaxId { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public IReadOnlyCollection<Sale> Sales { get; private set; } = []; //ok
    public IReadOnlyCollection<Return> Returns { get; private set; } = []; //ok
    public Address Address { get; private set; } = null!;

    public Client(string clientName, Address address)
    {
        ClientId = Guid.NewGuid();
        ClientName = clientName;
        CreatedAt = DateTime.UtcNow;
        Address = address;
    }

    public void Update(string clientName, Address address)
    {
        ClientName = clientName;
        UpdatedAt = DateTime.UtcNow;
        Address = address;
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;
}
