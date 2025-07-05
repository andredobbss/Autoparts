using FluentValidation;

namespace Autoparts.Api.Features.Sales.Domain;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.InvoiceNumber)
            .NotEmpty().WithMessage("InvoiceNumber cannot be empty.")
            .MaximumLength(100).WithMessage("InvoiceNumber must not exceed 100 characters.");


    }
}
