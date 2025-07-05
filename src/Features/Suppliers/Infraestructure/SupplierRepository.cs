using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Suppliers.Infraestructure;

public class SupplierRepository : ISupplierRepository
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
        return await _context.Suppliers.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Suppliers.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(supplier);
        if (result.IsValid is false)
            return result;

        await _context.Suppliers.AddAsync(supplier, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(supplier);
        if (result.IsValid is false)
            return result;

        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<bool> DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
       var result = _context.Suppliers.Remove(supplier);
        if (result is null)
            return false;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
