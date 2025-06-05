using FluentValidation;

namespace Autoparts.Api.Features.Suppliers.Domain;

public class SupplierValidator : AbstractValidator<Supplier>
{
    public SupplierValidator()
    {
        RuleFor(s => s.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");
        RuleFor(s => s.TaxId).IsValidCNPJ()
            .NotEmpty().WithMessage("Tax ID is required.")
            .MaximumLength(20).WithMessage("Tax ID must not exceed 20 characters.");
        RuleFor(s => s.Address)
            .NotNull().WithMessage("Address is required.");
    }
}

