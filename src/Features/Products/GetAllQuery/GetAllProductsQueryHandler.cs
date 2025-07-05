using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, PagedResponse<Product>>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<PagedResponse<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new PagedResponse<Product>(product);
    }
}