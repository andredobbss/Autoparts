using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;


namespace Autoparts.Api.Features.Categories.Infraestructure;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Category> _validator;

    public CategoryRepository(AutopartsDbContext context, IValidator<Category> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Categories.AsNoTracking().ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(category);

        if (result.IsValid is false)
            return result;

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(category);

        if (result.IsValid is false)
            return result;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<bool> DeleteAsync(Category category, CancellationToken cancellationToken)
    {
      
        var result = _context.Categories.Remove(category);

        if (result is null)     
            return false;
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
