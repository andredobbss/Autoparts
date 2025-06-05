using FluentValidation;

namespace Autoparts.Api.Features.Products.Domain;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.TechnicalDescription).NotNull().WithMessage("Description is not null");
        RuleFor(p => p.Compatibility).NotNull().WithMessage("Compatibility is not null");
        RuleFor(p => p.AcquisitionCost).GreaterThan(0).WithMessage("Acquisition cost must be greater than zero");
        RuleFor(p => p.CategoryId).NotNull().WithMessage("Category ID is not null");
        RuleFor(p => p.ManufacturerId).NotNull().WithMessage("Manufacturer ID is not null");
        RuleFor(p => p.SKU).NotEmpty().WithMessage("SKU cannot be empty");
    }
}
