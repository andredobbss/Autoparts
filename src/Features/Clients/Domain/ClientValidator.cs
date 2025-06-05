using FluentValidation;

namespace Autoparts.Api.Features.Clients.Domain
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.ClientName)
                .NotEmpty().NotNull().WithMessage("Name cannot be null or empty.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
            RuleFor(c => c.TaxId).IsValidCPF()
                .NotEmpty().NotNull().WithMessage("Tax ID cannot be null or empty.")
                .MaximumLength(20).WithMessage("Tax ID must not exceed 20 characters.");
        }
    }

}
