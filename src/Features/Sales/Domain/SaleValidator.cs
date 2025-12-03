using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Sales.Domain;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.InvoiceNumber)
            .NotEmpty()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY)
            .MaximumLength(100)
            .WithMessage(Resource.MAX_LENGTH_100);


    }
}
