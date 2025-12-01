using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            throw new KeyNotFoundException(string.Format(Resource.PRODUCTS_NOT_FOUND, request.ProductId));

        return
           new GetProductByIdQueryResponse
           (
               product!.ProductId,
               product.Name,
               product.TechnicalDescription,
               product.SKU,
               product.Compatibility,
               product.AcquisitionCost,
               product.SellingPrice,
               product.Quantity,
               product.Stock,
               product.StockStatus,
               product.Sales.Select(s => (DateTime.UtcNow - s.CreatedAt).Days > 90 ? EStockStatusOverTime.StagnantStock : EStockStatusOverTime.Active).FirstOrDefault(),
               product.Category.Description,
               product.Manufacturer.Description,
               product.CreatedAt);
    }
}