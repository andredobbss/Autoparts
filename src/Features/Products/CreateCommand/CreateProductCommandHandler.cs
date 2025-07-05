using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
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

        var product = new Product(request.Name, request.TechnicalDescription, request.Compatibility, request.AcquisitionCost, sku, request.CategoryId, request.ManufacturerId);

        var result = await _productRepository.AddAsync(product, cancellationToken);

        return result;
    }
}