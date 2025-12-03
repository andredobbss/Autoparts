using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.CreateCommand;

public sealed class CreateProductCommandHandler(IProductRepository productRepository, ISkuGenerator skuGenerator) : IRequestHandler<CreateProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository = productRepository;

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

        var result = await _productRepository.AddAsync(product, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _productRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}