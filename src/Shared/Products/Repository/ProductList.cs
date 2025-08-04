using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Products.Repository;

public class ProductList(AutopartsDbContext context) : IProductList
{
    private readonly AutopartsDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsListAsync(IEnumerable<SharedProductsDto> products, CancellationToken cancellationToken)
    {
        var productIds = products.Select(p => p.ProductId);

        // Consulta em lote para evitar múltiplos FindAsync (N chamadas no banco)
        var productsFromDb = _context.Products.AsNoTracking().Where(p => productIds.Contains(p.ProductId) && (p.StockStatus != EStockStatus.Backordered || p.StockStatus != EStockStatus.None));

        // Monta a lista final
        var productsResponse = products.Select(async productDto =>
        {
            var productEntity = await productsFromDb.FirstOrDefaultAsync(p => p.ProductId == productDto.ProductId, cancellationToken);

            return new Product(
                productDto.ProductId == Guid.Empty ? productEntity.ProductId : productDto.ProductId,
                productEntity.Name,
                productEntity.TechnicalDescription,
                productEntity.Compatibility,
                productEntity.SKU,
                productDto.Quantity,
                productEntity.Stock,
                productEntity?.StockStatus ?? EStockStatus.None,
                productEntity.CreatedAt,
                productEntity?.AcquisitionCost ?? 0m,
                productEntity?.SellingPrice ?? 0m,
                productEntity.CategoryId,
                productEntity.ManufacturerId);
        });

        return await Task.WhenAll(productsResponse);
    }
}
