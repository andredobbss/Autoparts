using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, PagedResponse<GetAllProductsQueryResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<PagedResponse<GetAllProductsQueryResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = products.
                     Select(p => new GetAllProductsQueryResponse
                     (
                         p.ProductId,
                         p.Name,
                         p.TechnicalDescription,
                         p.SKU,
                         p.Compatibility,
                         p.AcquisitionCost,
                         p.SellingPrice,
                         p.Quantity,
                         p.Stock,
                         p.StockStatus,
                         p.Sales.Select(s =>  (DateTime.UtcNow - s.CreatedAt).Days > 90? EStockStatusOverTime.StagnantStock : EStockStatusOverTime.Active).FirstOrDefault(),
                         p.Category.Description,
                         p.Manufacturer.Description,
                         p.CreatedAt));

        return new PagedResponse<GetAllProductsQueryResponse>(pagedResponse);
    }
}