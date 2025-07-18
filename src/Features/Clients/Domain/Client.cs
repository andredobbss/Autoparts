using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Clients.Domain;

public sealed class Client
{
    private readonly ClientValidator _clientValidation = new();
    private Client() { }

    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    public ICollection<Sale> Sales { get; private set; } = []; //ok
    public ICollection<Return> Returns { get; private set; } = []; //ok
    public Address Address { get; private set; } = null!;

    public Client(string clientName, Address address)
    {
        ClientId = Guid.NewGuid();
        ClientName = clientName;
        CreatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = ClientResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);

    }

    public void Update(string clientName, Address address)
    {
        ClientName = clientName;
        UpdatedAt = DateTime.UtcNow;
        Address = address;

       
        var validationResult = ClientResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ClientResult()
    {
        return _clientValidation.Validate(this);
    }
}
