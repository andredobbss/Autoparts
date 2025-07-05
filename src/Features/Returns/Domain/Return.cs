using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Exceptions;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Returns.Domain;

public sealed class Return
{
    private readonly ReturnValidator _returnValidator = new();
    private Return() { }
 
    public Guid ReturnId { get; private set; }
    public string Justification { get; private set; } = string.Empty;
    public string InvoiceNumber { get; private set; } = string.Empty;
    public bool Loss { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;
    public Guid UserId { get; private set; }
    public Guid ClientId { get; private set; } // ok

    public User User { get; private set; } = null!; //ok
    public Client Client { get; private set; } = null!; // ok
    public ICollection<Product> Products { get; private set; } = [];
    public ICollection<ReturnProduct> ReturnProducts { get; private set; } = [];
    public Return(string justification, string invoiceNumber, bool loss, Guid userId, Guid clientId)
    {
        ReturnId = Guid.NewGuid();
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Loss = loss;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;

        if (ReturnResult().IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ReturnResult().Errors);
    }

    public void Update(string justification, string invoiceNumber, bool loss, Guid userId, Guid clientId)
    {
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Loss = loss;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;

        if (ReturnResult().IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, ReturnResult().Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ReturnResult()
    {
        return _returnValidator.Validate(this);
    }

}
