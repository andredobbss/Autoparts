using Autoparts.Api.Features.Suppliers.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Suppliers.Infraestructure;

public interface ISupplierRepository
{
    Task<IPagedList<Supplier>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Supplier supplier, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Supplier supplier, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
