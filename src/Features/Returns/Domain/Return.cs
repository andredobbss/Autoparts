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
    public Return(Guid returnId, string justification, string invoiceNumber, bool loss, Guid userId, Guid clientId, ICollection<ReturnProduct> returnProducts)
    {
        ReturnId = returnId;
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Loss = loss;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        if (loss is false)
            ReturnProducts = returnProducts;

        var validationResult = ReturnResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string justification, string invoiceNumber, bool loss, Guid userId, Guid clientId, ICollection<ReturnProduct> returnProducts)
    {
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        Loss = loss;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        if (loss is false)
            ReturnProducts = returnProducts;

        var validationResult = ReturnResult();
        if (validationResult.IsValid is false)
            throw new DomainValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ReturnResult()
    {
        return _returnValidator.Validate(this);
    }

}
