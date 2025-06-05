using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Manufacturers.Infraestructure;

public class ManufacturerRepository
{
    private readonly AutopartsDbContext _context;

    public ManufacturerRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        return await _context.Manufacturers.ToListAsync();
    }

    public async Task<Manufacturer?> GetByIdAsync(int id)
    {
        return await _context.Manufacturers.FindAsync(id);
    }

    public async Task AddAsync(Manufacturer manufacturer)
    {
        await _context.Manufacturers.AddAsync(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Manufacturer manufacturer)
    {
        var existingManufacturer = await _context.Manufacturers.FindAsync(manufacturer.ManufacturerId) ??
            throw new KeyNotFoundException($"Manufacturer with ID {manufacturer.ManufacturerId} not found.");

        _context.Manufacturers.Update(existingManufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Manufacturer manufacturer)
    {
        var existingManufacturer = await _context.Manufacturers.FindAsync(manufacturer.ManufacturerId) ??
            throw new KeyNotFoundException($"Manufacturer with ID {manufacturer.ManufacturerId} not found.");

        _context.Manufacturers.Remove(existingManufacturer);
        await _context.SaveChangesAsync();
    }
}
