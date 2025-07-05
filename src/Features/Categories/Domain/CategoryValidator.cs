using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Categories.Domain;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage(Resource.NOT_NULL_OR_EMPTY)
            .MaximumLength(100)
            .WithMessage(Resource.MAX_LENGTH_100);
    }
}
