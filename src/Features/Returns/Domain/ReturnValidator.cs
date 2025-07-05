using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Returns.Domain
{
    public class ReturnValidator : AbstractValidator<Return>
    {
        public ReturnValidator()
        {
            RuleFor(r => r.Justification)
                .NotEmpty()
                .NotNull()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY)
                .MaximumLength(255)
                .WithMessage(Resource.MAX_LENGTH_255);
            RuleFor(r => r.InvoiceNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY)
                .MaximumLength(50)
                .WithMessage(Resource.MAX_LENGTH_50);
            RuleFor(r => r.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY);
            RuleFor(r => r.ClientId)
                .NotEmpty()
                .WithMessage(Resource.NOT_NULL_OR_EMPTY);
        }
    }
}
