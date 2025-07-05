using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;


namespace Autoparts.Api.Features.Products.Infraestructure;

public class ProductRepository : IProductRepository
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Product> _validator;

    public ProductRepository(AutopartsDbContext context, IValidator<Product> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IPagedList<Product>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return  await _context.Products.Where(p => ids.Contains(p.ProductId)).ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products.FindAsync(id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Product product, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(product);

        if (result.IsValid is false)
            return result;

        await _context.Products.AddAsync(product, cancellationToken);;
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(product);

        if (result.IsValid is false)
            return result;

        _context.Products.Update(product);
        return result;
    }

    public async Task<bool> DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        var result = _context.Products.Remove(product);

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
