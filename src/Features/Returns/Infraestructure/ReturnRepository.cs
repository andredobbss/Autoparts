using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.Infraestructure;

public class ReturnRepository : IReturnRepository, IDisposable
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Return> _validator;

    public ReturnRepository(AutopartsDbContext context, IValidator<Return> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Return>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Returns.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Return?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Returns.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(returnItem, cancellationToken);

        if (result.IsValid is false)
            return result;

        await _context.Returns.AddAsync(returnItem, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(returnItem, cancellationToken);

        if (result.IsValid is false)
            return result;

        _context.Returns.Update(returnItem);
        return result;
    }

    public async Task<bool> DeleteAsync(Return returnItem, CancellationToken cancellationToken)
    {
        var result = _context.Returns.Remove(returnItem);
        if (result is null)
            return false;

        return true;
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
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
