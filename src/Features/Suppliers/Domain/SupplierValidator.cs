using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Suppliers.Domain;

public class SupplierValidator : AbstractValidator<Supplier>
{
    public SupplierValidator()
    {
        RuleFor(s => s.CompanyName)
            .NotEmpty()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY)
            .MaximumLength(255)
            .WithMessage(Resource.MAX_LENGTH_255);

        RuleFor(s => s.Email)
                .EmailAddress()
                .When(s => !string.IsNullOrEmpty(s.Email))
                .WithMessage(Resource.INVALID_EMAIL);

        When(s => s.TaxIdType == ETaxIdType.CPF, () =>
        {
            RuleFor(s => s.TaxId)
                .IsValidCPF()
                .When(s => !string.IsNullOrEmpty(s.TaxId))
                .WithMessage(Resource.INVALID_CPF);
        });

        When(s => s.TaxIdType == ETaxIdType.CNPJ, () =>
        {
            RuleFor(s => s.TaxId)
                .IsValidCNPJ()
                .When(s => !string.IsNullOrEmpty(s.TaxId))
                .WithMessage(Resource.INVALID_CNPJ);
        });
    }
}

