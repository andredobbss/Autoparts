using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
    }
}