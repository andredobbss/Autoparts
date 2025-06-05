using FluentValidation;

namespace Autoparts.Api.Shared.ValueObjects;

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
        RuleFor(a => a.ZipCode)
            .NotEmpty().NotNull().WithMessage("ZipCode cannot be null or empty.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("ZipCode must be in the format '12345' or '12345-6789'.");
    }
}
