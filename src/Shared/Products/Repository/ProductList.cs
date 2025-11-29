using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Dto;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Products.Repository;

public class ProductList(AutopartsDbContext context) : IProductList
{
    private readonly AutopartsDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsListAsync(IEnumerable<SharedProductsDto> products, CancellationToken cancellationToken)
    {
        var productIds = products.Select(p => p.ProductId).ToList();

        // 1) Uma única consulta ao banco
        var productsFromDb = await _context.Products!
            .AsNoTracking()
            .Where(p => productIds.Contains(p.ProductId))
            .ToListAsync(cancellationToken);

        // 2) Índice para lookup em memória
        var dict = productsFromDb.ToDictionary(p => p.ProductId);

        // 3) Construção final SEM async dentro do foreach
        var response = new List<Product>();

        foreach (var dto in products)
        {
            if (!dict.TryGetValue(dto.ProductId, out var productEntity))
                continue;

            response.Add(new Product(
                dto.ProductId == Guid.Empty ? productEntity.ProductId : dto.ProductId,
                productEntity.Name,
                productEntity.TechnicalDescription,
                productEntity.Compatibility,
                productEntity.SKU,
                dto.Quantity,
                productEntity.Stock,
                productEntity.CreatedAt,
                productEntity.AcquisitionCost,
                productEntity.SellingPrice,
                productEntity.CategoryId,
                productEntity.ManufacturerId
            ));
        }

        return response;
    }
}
