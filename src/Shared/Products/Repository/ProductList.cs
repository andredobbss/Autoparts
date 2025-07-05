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
        var productIds = products.Select(p => p.ProductId).ToList();

        // Consulta em lote para evitar múltiplos FindAsync (N chamadas no banco)
        var productsFromDb = await _context.Products.AsNoTracking().Where(p => productIds.Contains(p.ProductId) && (p.StockStatus != EStockStatus.Backordered || p.StockStatus != EStockStatus.None)).ToListAsync(cancellationToken);

        // Monta a lista final
        var productsResponse = products.Select(productDto =>
        {
            var productEntity = productsFromDb.FirstOrDefault(p => p.ProductId == productDto.ProductId);

            return new Product(
                productDto.ProductId,
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
        }).ToList();

        return productsResponse;
    }
}
