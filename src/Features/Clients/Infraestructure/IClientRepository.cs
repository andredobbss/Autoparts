using Autoparts.Api.Features.Clients.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.Infraestructure;

public interface IClientRepository
{
    Task<IPagedList<Client>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Client client, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Client client, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Client client, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
