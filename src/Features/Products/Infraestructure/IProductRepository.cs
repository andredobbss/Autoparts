using Autoparts.Api.Features.Products.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Products.Infraestructure;

public interface IProductRepository
{
    Task<IPagedList<Product>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Product product, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
