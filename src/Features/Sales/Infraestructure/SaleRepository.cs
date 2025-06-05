using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Sales.Infraestructure;

public class SaleRepository
{
    private readonly AutopartsDbContext _context;

    public SaleRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales.FindAsync(id);
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sale sale)
    {
        var existingSale = await _context.Sales.FindAsync(sale.SaleId) ??
            throw new KeyNotFoundException($"Sale with ID {sale.SaleId} not found.");

        _context.Sales.Update(existingSale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sale sale)
    {
        var existingSale = await _context.Sales.FindAsync(sale.SaleId) ??
            throw new KeyNotFoundException($"Sale with ID {sale.SaleId} not found.");

        _context.Sales.Remove(existingSale);
        await _context.SaveChangesAsync();
    }
}
