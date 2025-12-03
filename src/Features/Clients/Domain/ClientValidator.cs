using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Clients.Domain
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.ClientName)
                .NotEmpty()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY)
                .MaximumLength(100)
                .WithMessage(Resource.MAX_LENGTH_100);

            RuleFor(c => c.Email)
                .MaximumLength(255)
                .WithMessage(Resource.MAX_LENGTH_255)
                .EmailAddress()
                .When(c => !string.IsNullOrEmpty(c.Email))
                .WithMessage(Resource.INVALID_EMAIL);

            When(c => c.TaxIdType == ETaxIdType.CPF, () =>
            {
                RuleFor(c => c.TaxId)
                    .IsValidCPF()
                    .When(c => !string.IsNullOrEmpty(c.TaxId))
                    .WithMessage(Resource.INVALID_CPF);
            });

            When(c => c.TaxIdType == ETaxIdType.CNPJ, () =>
            {
                RuleFor(c => c.TaxId)
                    .IsValidCNPJ()
                    .When(c => !string.IsNullOrEmpty(c.TaxId))
                    .WithMessage(Resource.INVALID_CNPJ);
            });

        }
    }

}
