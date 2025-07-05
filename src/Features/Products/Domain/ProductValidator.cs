using Autoparts.Api.Shared.Resources;
using FluentValidation;

namespace Autoparts.Api.Features.Products.Domain;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY).MaximumLength(100).WithMessage(Resource.MAX_LENGTH_100);
        RuleFor(p => p.TechnicalDescription).NotNull().WithMessage(Resource.NOT_NULL_OR_EMPTY).MaximumLength(255).WithMessage (Resource .MAX_LENGTH_255);
        RuleFor(p => p.Compatibility).NotNull().NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY).MaximumLength (50).WithMessage (Resource .MAX_LENGTH_50);
        RuleFor(p => p.AcquisitionCost).GreaterThan(0).WithMessage(Resource.GREATER_THAN_ZERO).NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY);
        RuleFor(p => p.CategoryId).NotNull().NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY);
        RuleFor(p => p.ManufacturerId).NotNull().NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY);
        //RuleFor(p => (int)p.Quantity).GreaterThanOrEqualTo(0).WithMessage(Resource.GREATER_THAN_ZERO).NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY); 
        RuleFor(p => p.SKU).NotNull().NotEmpty().WithMessage(Resource.NOT_NULL_OR_EMPTY);
    }
}
