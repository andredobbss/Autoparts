using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Manufacturers.Domain;

public class ManufacturerValidator : AbstractValidator<Manufacturer>
{
    public ManufacturerValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY)
            .MaximumLength(100)
            .WithMessage(Resource.MAX_LENGTH_100);
    }
}

