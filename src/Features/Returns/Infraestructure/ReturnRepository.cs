using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Returns.Infraestructure;

public class ReturnRepository
{
    private readonly AutopartsDbContext _context;

    public ReturnRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Return>> GetAllAsync()
    {
        return await _context.Returns.ToListAsync();
    }

    public async Task<Return?> GetByIdAsync(int id)
    {
        return await _context.Returns.FindAsync(id);
    }

    public async Task AddAsync(Return returnItem)
    {
        await _context.Returns.AddAsync(returnItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Return returnItem)
    {
        var existingReturn = await _context.Returns.FindAsync(returnItem.ReturnId) ??
            throw new KeyNotFoundException($"Return with ID {returnItem.ReturnId} not found.");

        _context.Returns.Update(existingReturn);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Return returnItem)
    {
        var existingReturn = await _context.Returns.FindAsync(returnItem.ReturnId) ??
            throw new KeyNotFoundException($"Return with ID {returnItem.ReturnId} not found.");

        _context.Returns.Remove(existingReturn);
        await _context.SaveChangesAsync();
    }
}
