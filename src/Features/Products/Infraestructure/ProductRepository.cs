using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;


namespace Autoparts.Api.Features.Products.Infraestructure;

public class ProductRepository
{
    private readonly AutopartsDbContext _context;

    private readonly IValidator<Product> _validator;

    public ProductRepository(AutopartsDbContext context, IValidator<Product> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<ValidationResult> AddAsync(Product product)
    {
        var result = _validator.Validate(product);

        if (!result.IsValid)
            return result;

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.ProductId) ??
            throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.ProductId) ??
            throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");

        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();
    }
}
