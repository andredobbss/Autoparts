using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.Infraestructure;

public class PurchaseRepository : IPurchaseRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IStockCalculator _stockCalculator;

    private readonly IValidator<Purchase> _validator;

    public PurchaseRepository(AutopartsDbContext context, IValidator<Purchase> validator, IStockCalculator stockCalculator)
    {
        _context = context;
        _validator = validator;
        _stockCalculator = stockCalculator;
    }

    public async Task<IPagedList<Purchase>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Purchases!.AsNoTracking()
                                        .Include(p => p.User)
                                        .Include(p => p.Supplier)
                                        .Include(P => P.PurchaseProducts)
                                        .ThenInclude(pp => pp.Product)
                                        .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Purchases!.Include(p => p.User)
                                        .Include(p => p.Supplier)
                                        .Include(P => P.PurchaseProducts)
                                        .ThenInclude(pp => pp.Product)
                                        .FirstOrDefaultAsync(p => p.PurchaseId == id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(purchase, cancellationToken);
        if (!result.IsValid)
            return result;

        await _context.Purchases!.AddAsync(purchase, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(purchase, cancellationToken);
        if (!result.IsValid)
            return result;

        _context.Purchases!.Update(purchase).State = EntityState.Modified;
        return result;
    }

    public async Task<bool> DeleteAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        _context.Purchases!.Update(purchase);

        return true;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await _context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await _stockCalculator.StockCalculateAsync(cancellationToken);

            if (commitResult > 0 && stockUpdated)
            {
                await transaction.CommitAsync(cancellationToken);
                return true;
            }

            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }


}
