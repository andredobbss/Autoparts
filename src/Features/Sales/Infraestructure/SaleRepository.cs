using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Sales.Infraestructure;

public class SaleRepository : ISaleRepository
{
    private readonly AutopartsDbContext _context;
    private readonly IValidator<Sale> _alidator;

    public SaleRepository(AutopartsDbContext context, IValidator<Sale> alidator)
    {
        _context = context;
        _alidator = alidator;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Sales.ToListAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Sale sale, CancellationToken cancellationToken)
    {
        var validationResult = await _alidator.ValidateAsync(sale, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult;

        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return validationResult;
    }

    public async Task<ValidationResult> UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var validationResult = await _alidator.ValidateAsync(sale, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult;

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return validationResult;
    }

    public async Task<bool> DeleteAsync(Sale sale, CancellationToken cancellationToken)
    {
        var existingSale = _context.Sales.Remove(sale);
        if (existingSale is null)
            return false;
 
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
