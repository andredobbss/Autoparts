using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Shared.ValueObejct;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty().NotNull().WithMessage("Street cannot be null or empty.")
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");
        RuleFor(a => a.City)
            .NotEmpty().NotNull().WithMessage("City cannot be null or empty.")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");
        RuleFor(a => a.State)
            .NotEmpty().NotNull().WithMessage("State cannot be null or empty.")
            .MaximumLength(50).WithMessage("State must not exceed 50 characters.");
    //    RuleFor(a => a.ZipCode)
    //        .NotEmpty().NotNull().WithMessage("ZipCode cannot be null or empty.")
    //        .Matches(@"^\d{5}(-\d{4})?$").WithMessage("ZipCode must be in the format '12345' or '12345-6789'.");

    //    RuleFor(c => c.TaxId)
    //.NotEmpty()
    //.NotNull()
    //.WithMessage(Resource.NOT_NULL_OR_EMPTY);

    //    RuleFor(c => c.TaxId)
    //        .Must(taxId => taxId.Length == 11)
    //        .When(c => c.TaxId?.Length == 11, ApplyConditionTo.CurrentValidator)
    //        .WithMessage(Resource.INVALID_CPF)
    //        .IsValidCPF()
    //        .When(c => c.TaxId?.Length == 11, ApplyConditionTo.CurrentValidator);

    //    RuleFor(c => c.TaxId)
    //        .Must(taxId => taxId.Length == 14)
    //        .When(c => c.TaxId?.Length == 14, ApplyConditionTo.CurrentValidator)
    //        .WithMessage(Resource.INVALID_CNPJ)
    //        .IsValidCNPJ()
    //        .When(c => c.TaxId?.Length == 14, ApplyConditionTo.CurrentValidator);
    }
}
