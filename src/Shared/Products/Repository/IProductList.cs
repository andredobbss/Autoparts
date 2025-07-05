using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Products.Dto;

namespace Autoparts.Api.Shared.Products.Repository;

public interface IProductList
{
    Task<IEnumerable<Product>> GetProductsListAsync(IEnumerable<SharedProductsDto> products, CancellationToken cancellationToken);
}
