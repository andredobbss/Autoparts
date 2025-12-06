using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.UpdateCommand;
public sealed class UpdateProductCommandHandler(AutopartsDbContext context) : IRequestHandler<UpdateProductCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products!.FindAsync(request.ProductId, cancellationToken);
        if (product is null)
            return new ValidationResult { Errors = { new ValidationFailure(Resource.PRODUCT, string.Format(Resource.PRODUCTS_NOT_FOUND, request.ProductId)) } };

        product.Update(request.Name,
                        request.TechnicalDescription,
                        request.Compatibility,
                        request.AcquisitionCost,
                        request.CategoryId,
                        request.ManufacturerId);

        _context.Products!.Update(product);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
        {
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        return new ValidationResult();
    }
}