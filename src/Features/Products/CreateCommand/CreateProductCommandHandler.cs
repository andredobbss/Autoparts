using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using MediatR;
namespace Autoparts.Api.Features.Products.CreateCommand;
public sealed class CreateProductCommandHandler(ProductRepository productRepository, SkuGenerator skuGenerator) : IRequestHandler<CreateProductCommand, Product>
{
    private readonly ProductRepository _productRepository = productRepository;

    private readonly SkuGenerator _skuGenerator = skuGenerator;

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
       var product = new Product(request.TechnicalDescription, request.Compatibility, request.AcquisitionCost, request.CategoryId, request.ManufacturerId);

        var sku = await _skuGenerator.GenerateSKUAsync(request.ManufacturerId, request.CategoryId);

        product.SetSku(sku);

        var result = await _productRepository.AddAsync(product);

        if (result.IsValid is true)
            return product;

        throw new ArgumentException($"Invalid product data: {result.ToDictionary()}");
    }
}