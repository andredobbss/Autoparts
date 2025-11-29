using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Returns.Domain;

public sealed class Return
{
    private readonly ReturnValidator _returnValidator = new();
    private Return() { }

    public Guid ReturnId { get; private set; }
    public string Justification { get; private set; } = string.Empty;
    public string InvoiceNumber { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public DateTime? DeletedAt { get; private set; } = null;
    public Guid UserId { get; private set; }
    public Guid ClientId { get; private set; }

    public User User { get; private set; } = null!;
    public Client Client { get; private set; } = null!;
    public ICollection<Product> Products { get; private set; } = [];
    public ICollection<ReturnProduct> ReturnProducts { get; private set; } = [];

    public Return(Guid returnId, string justification, string invoiceNumber, Guid userId, Guid clientId, ICollection<ReturnProduct> returnProducts)
    {
        ReturnId = returnId;
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        ReturnProducts = returnProducts;

        var validationResult = ReturnResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Update(string justification, string invoiceNumber, Guid userId, Guid clientId, ICollection<ReturnProduct> returnProducts)
    {
        Justification = justification;
        InvoiceNumber = invoiceNumber;
        UpdatedAt = DateTime.UtcNow;
        UserId = userId;
        ClientId = clientId;
        ReturnProducts = returnProducts;

        var validationResult = ReturnResult();
        if (validationResult.IsValid is false)
            throw new ValidationException(Resource.ERROR_DOMAIN, validationResult.Errors);
    }

    public void Delete() => DeletedAt = DateTime.UtcNow;

    private ValidationResult ReturnResult()
    {
        return _returnValidator.Validate(this);
    }

}
