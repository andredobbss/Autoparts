using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Purchases.Infraestructure;

public class PurchaseRepository
{
    private readonly AutopartsDbContext _context;

    public PurchaseRepository(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _context.Purchases.ToListAsync();
    }

    public async Task<Purchase?> GetByIdAsync(int id)
    {
        return await _context.Purchases.FindAsync(id);
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _context.Purchases.AddAsync(purchase);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Purchase purchase)
    {
        var existingPurchase = await _context.Purchases.FindAsync(purchase.PurchaseId) ??
            throw new KeyNotFoundException($"Purchase with ID {purchase.PurchaseId} not found.");
        _context.Purchases.Update(existingPurchase);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Purchase purchase)
    {
        var existingPurchase = await _context.Purchases.FindAsync(purchase.PurchaseId) ??
            throw new KeyNotFoundException($"Purchase with ID {purchase.PurchaseId} not found.");

        _context.Purchases.Remove(existingPurchase);
        await _context.SaveChangesAsync();
    }
}
