using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Clients.Domain;

public sealed class Client
{
    private readonly ClientValidator _clientValidation = new();

    private Client() { }

    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = null!;
    public string? Email { get; private set; }
    public ETaxIdType? TaxIdType { get; private set; }
    public string? TaxId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;

    // Navigation Properties
    public Address Address { get; private set; } = null!;
    public ICollection<Sale> Sales { get; private set; } = [];
    public ICollection<Return> Returns { get; private set; } = [];

    public Client(string clientName,
                  Address address,
                  string? email = null,
                  ETaxIdType? taxIdType = null,
                  string? taxId = null)
    {
        ClientId = Guid.NewGuid();
        ClientName = clientName;
        TaxIdType = taxIdType;
        Email = email;
        TaxId = taxId;
        CreatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = ClientResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
        Email = email;
    }

    public void Update(string clientName,
                       Address address,
                       string? email = null,
                       ETaxIdType? taxIdType = null,
                       string? taxId = null)
    {
        ClientName = clientName;
        TaxIdType = taxIdType;
        Email = email;
        TaxId = taxId;
        UpdatedAt = DateTime.UtcNow;
        Address = address;

        var validationResult = ClientResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ClientResult()
    {
        return _clientValidation.Validate(this);
    }
}
