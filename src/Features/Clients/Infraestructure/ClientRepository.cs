using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.Infraestructure;

public class ClientRepository : IClientRepository, IDisposable
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
        return await _context.Clients!.AsNoTracking()
                                       .Include(c => c.Address)
                                       .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Clients!.Include(c => c.Address)
                             .FirstOrDefaultAsync(c => c.ClientId == id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Client client, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(client, cancellationToken);
        if (!result.IsValid)
            return result;

        await _context.Clients!.AddAsync(client, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Client client, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(client, cancellationToken);
        if (!result.IsValid)
            return result;

        _context.Clients!.Update(client);
        return result;
    }

    public async Task<bool> DeleteAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients!.Update(client);

        return true;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
            return false;

        return true;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
