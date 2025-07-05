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
                .NotNull()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY)
                .MaximumLength(50)
                .WithMessage(Resource.MAX_LENGTH_100);
           
        }
    }

}
