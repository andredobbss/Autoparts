using Autoparts.Api.Features.Sales.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Sales.Infraestructure;

public interface ISaleRepository
{
    Task<IPagedList<Sale>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Sale sale, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Sale sale, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Sale sale, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
