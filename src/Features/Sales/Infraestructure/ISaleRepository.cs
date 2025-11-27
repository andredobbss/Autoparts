using Autoparts.Api.Features.Sales.Domain;
using FluentValidation.Results;

namespace Autoparts.Api.Features.Sales.Infraestructure;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Sale sale, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Sale sale, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Sale sale, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
