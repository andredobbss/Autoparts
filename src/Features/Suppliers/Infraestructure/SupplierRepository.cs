using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Suppliers.Infraestructure;

public class SupplierRepository : ISupplierRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Supplier> _validator;
    public SupplierRepository(AutopartsDbContext context, IValidator<Supplier> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Supplier>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Suppliers!.AsNoTracking().ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Suppliers!.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(supplier, cancellationToken);
        if (!result.IsValid)
            return result;

        await _context.Suppliers!.AddAsync(supplier, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(supplier, cancellationToken);
        if (!result.IsValid)
            return result;

        _context.Suppliers!.Update(supplier);
        return result;
    }

    public async Task<bool> DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _context.Suppliers!.Update(supplier);

        return true;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
            return false;

        return true;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
