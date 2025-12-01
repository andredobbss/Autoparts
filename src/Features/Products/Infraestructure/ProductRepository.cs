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
        var products = await _context.Products!.AsNoTracking()
                                               .Include(p => p.Category)
                                               .Include(p => p.Manufacturer)
                                               .Include(p => p.Sales)
                                               .ToPagedListAsync(pageNumber, pageSize, cancellationToken);

        return products;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products!
                             .Include(p => p.Category)
                             .Include(p => p.Manufacturer)
                             .Include(p => p.Sales)
                             .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
    }

    public async Task<ValidationResult> AddAsync(Product product, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(product, cancellationToken);

        if (!result.IsValid)
            return result;

        await _context.Products!.AddAsync(product, cancellationToken);
        return result;
    }

    public async Task<ValidationResult> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(product, cancellationToken);

        if (!result.IsValid)
            return result;

        _context.Products!.Update(product);
        return result;
    }

    public async Task<bool> DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products!.Update(product);

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
