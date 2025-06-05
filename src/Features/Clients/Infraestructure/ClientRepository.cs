using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Clients.Infraestructure;

public class ClientRepository
{
    private readonly AutopartsDbContext _context;

    public ClientRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task AddAsync(Client client)
    {
        var existingClient = await _context.Clients.FindAsync(client.ClientId) ??
           throw new KeyNotFoundException($"Client with ID {client.ClientId} not found.");

        await _context.Clients.AddAsync(existingClient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        var existingClient = await _context.Clients.FindAsync(client.ClientId) ?? 
            throw new KeyNotFoundException($"Client with ID {client.ClientId} not found.");

        _context.Clients.Update(existingClient);
        await _context.SaveChangesAsync();
    }
}
