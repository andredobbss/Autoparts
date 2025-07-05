using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.Infraestructure;

public class ClientRepository : IClientRepository
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Client> _validator;

    public ClientRepository(AutopartsDbContext context, IValidator<Client> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Client>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Clients.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Clients.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Client client, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(client);

        if (result.IsValid is false)
            return result;

        await _context.Clients.AddAsync(client,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Client client, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(client);

        if (result.IsValid is false)
            return result;

        _context.Clients.Update(client);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<bool> DeleteAsync(Client client, CancellationToken cancellationToken)
    {
        var result = _context.Clients.Remove(client);

        if (result is null)
            return false;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
