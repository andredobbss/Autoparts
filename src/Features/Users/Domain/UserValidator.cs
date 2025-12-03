using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Users.Domain;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty()
            .WithMessage(Resource.NAME_IS_REQUIRED)
            .MaximumLength(100)
            .WithMessage(Resource.MAX_LENGTH_100);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(Resource.EMAIL_IS_REQUIRED)
            .EmailAddress()
            .WithMessage(Resource.INVALID_EMAIL)
            .MaximumLength(255)
            .WithMessage(Resource.MAX_LENGTH_255);

        When(user => user.TaxIdType == ETaxIdType.CPF, () =>
        {
            RuleFor(user => user.TaxId)
                .IsValidCPF()
                .When(user => !string.IsNullOrEmpty(user.TaxId))
                .WithMessage(Resource.INVALID_CPF);
        });

        When(user => user.TaxIdType == ETaxIdType.CNPJ, () =>
        {
            RuleFor(user => user.TaxId)
                .IsValidCNPJ()
                .When(user => !string.IsNullOrEmpty(user.TaxId))
                .WithMessage(Resource.INVALID_CNPJ);
        });
    }
}

