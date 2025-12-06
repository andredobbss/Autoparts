using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.CreateCommand;

public sealed class CreateProductCommandHandler(AutopartsDbContext context, ISkuGenerator skuGenerator) : IRequestHandler<CreateProductCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;

    private readonly ISkuGenerator _skuGenerator = skuGenerator;

    public async Task<ValidationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var sku = await _skuGenerator.GenerateSKUAsync(request.ManufacturerId, request.CategoryId, cancellationToken);
        if (string.IsNullOrWhiteSpace(sku))
            return new ValidationResult([new ValidationFailure(Resource.SKU, Resource.SKU_FAILED)]);

        var product = new Product(
            request.Name,
            request.TechnicalDescription,
            request.Compatibility,
            request.AcquisitionCost,
            sku,
            request.CategoryId,
            request.ManufacturerId);

        await _context.AddAsync(product, cancellationToken);

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