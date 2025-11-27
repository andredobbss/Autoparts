using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.Infraestructure;

public class ReturnRepository : IReturnRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IStockCalculator _stockCalculator;

    private readonly IValidator<Return> _validator;

    public ReturnRepository(AutopartsDbContext context, IValidator<Return> validator, IStockCalculator stockCalculator)
    {
        _context = context;
        _validator = validator;
        _stockCalculator = stockCalculator;
    }

    public async Task<IPagedList<Return>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Returns!.AsNoTracking().ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Return?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Returns!.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(returnItem, cancellationToken);

        if (!result.IsValid)
            return result;

        await _context.Returns!.AddAsync(returnItem, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(returnItem, cancellationToken);

        if (!result.IsValid)
            return result;

        _context.Returns!.Update(returnItem);
        return result;
    }

    public async Task<bool> DeleteAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = _context.Returns!.Update(returnItem);
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
