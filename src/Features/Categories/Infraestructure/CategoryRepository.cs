using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;


namespace Autoparts.Api.Features.Categories.Infraestructure;

public sealed class CategoryRepository
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Category> _validator;

    public CategoryRepository(AutopartsDbContext context, IValidator<Category> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        //return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id, cancellationToken);



        //return await _context.Categories.FindAsync(id, cancellationToken) ??
        //      throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {id}");


        return await _context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.CategoryId == id, cancellationToken) ??
              throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {id}");
    }

    public async Task<ValidationResult> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(category);

        if (!result.IsValid)
            return result;

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        //var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId, cancellationToken) ??
        //    throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} {category.CategoryId}");

        //var existingCategory = await _context.Categories.FindAsync(category.CategoryId, cancellationToken) ??
        //    throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {category.CategoryId}");

        var existingCategory = await _context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.CategoryId == category.CategoryId, cancellationToken) ??
            throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {category.CategoryId}");

        var result = _validator.Validate(existingCategory);

        if (!result.IsValid)
            return result;

        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<bool> DeleteAsync(Category category, CancellationToken cancellationToken)
    {

        //var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId, cancellationToken);

        var existingCategory = await _context.Categories.FindAsync(category.CategoryId, cancellationToken) ??
            throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {category.CategoryId}");

        bool verif;

        if (existingCategory is not null)
        {
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync(cancellationToken);
            verif = true;
        }
        else
        {
            throw new KeyNotFoundException($"{Resource.ID_NOT_FOUND} : {category.CategoryId}");
        }

        return verif;
    }
}
