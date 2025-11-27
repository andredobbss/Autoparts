using Autoparts.Api.Features.Returns.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.Infraestructure;

public interface IReturnRepository
{
    Task<IPagedList<Return>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Return?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Return returnItem, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Return returnItem, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Return returnItem, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
