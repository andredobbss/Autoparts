using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Manufacturers.Infraestructure;

public class ManufacturerRepository : IManufacturerRepository, IDisposable
{
    private readonly AutopartsDbContext _context;
    private readonly IValidator<Manufacturer> _validator;

    public ManufacturerRepository(AutopartsDbContext context, IValidator<Manufacturer> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Manufacturer>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Manufacturers.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Manufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Manufacturers!.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Manufacturer manufacturer, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(manufacturer);
        if (!result.IsValid)
            return result;

        await _context.Manufacturers!.AddAsync(manufacturer, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Manufacturer manufacturer, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(manufacturer);
        if (!result.IsValid)
            return result;

        _context.Manufacturers!.Update(manufacturer);
        return result;
    }

    public async Task<bool> DeleteAsync(Manufacturer manufacturer, CancellationToken cancellationToken)
    {
       var result = _context.Manufacturers!.Update(manufacturer);
        if (result is null)
            return false;

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
