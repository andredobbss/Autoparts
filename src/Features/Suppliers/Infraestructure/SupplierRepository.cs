using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Suppliers.Infraestructure;

public class SupplierRepository
{
    private readonly AutopartsDbContext _context;

    public SupplierRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers.FindAsync(id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        var existingSupplier = await _context.Suppliers.FindAsync(supplier.SupplierId) ??
            throw new KeyNotFoundException($"Supplier with ID {supplier.SupplierId} not found.");

        _context.Suppliers.Update(existingSupplier);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Supplier supplier)
    {
        var existingSupplier = await _context.Suppliers.FindAsync(supplier.SupplierId) ??
            throw new KeyNotFoundException($"Supplier with ID {supplier.SupplierId} not found.");

        _context.Suppliers.Remove(existingSupplier);
        await _context.SaveChangesAsync();
    }
}
