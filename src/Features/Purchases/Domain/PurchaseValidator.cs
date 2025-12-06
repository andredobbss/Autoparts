using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Purchases.Domain;

public class PurchaseValidator : AbstractValidator<Purchase>
{
    public PurchaseValidator()
    {
        RuleFor(p => p.InvoiceNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY)
            .MaximumLength(50)
            .WithMessage(Resource.MAX_LENGTH_50);
        RuleFor(p => p.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY);
        RuleFor(p => p.SupplierId)
            .NotEmpty()
            .NotNull()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY);
    }
}

