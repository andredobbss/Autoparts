using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Sales.Infraestructure;

public class SaleRepository : ISaleRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IStockCalculator _stockCalculator;

    private readonly IValidator<Sale> _alidator;

    public SaleRepository(AutopartsDbContext context, IValidator<Sale> alidator, IStockCalculator stockCalculator)
    {
        _context = context;
        _alidator = alidator;
        _stockCalculator = stockCalculator;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Sales!.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales!.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Sale sale, CancellationToken cancellationToken)
    {
        var result = await _alidator.ValidateAsync(sale, cancellationToken);

        if (!result.IsValid)
            return result;

        await _context.Sales!.AddAsync(sale, cancellationToken);

        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var result = await _alidator.ValidateAsync(sale, cancellationToken);

        if (!result.IsValid)
            return result;

        _context.Sales!.Update(sale);

        return result;
    }

    public async Task<bool> DeleteAsync(Sale sale, CancellationToken cancellationToken)
    {
        var result = _context.Sales!.Update(sale);
        if (result is null)
            return false;

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
