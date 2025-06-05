using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Products.Infraestructure;

public class SkuGenerator
{
    private readonly AutopartsDbContext _context;

    public SkuGenerator(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateSKUAsync(Guid manufacturerId, Guid categoryId)
    {
        //var manufacturer = await _context.Manufacturers.FindAsync(manufacturerId) ??
        //    throw new KeyNotFoundException("Manufacturer not found.");

        var manufacturer = await _context.Manufacturers.AsNoTracking().SingleOrDefaultAsync(m => m.ManufacturerId == manufacturerId) ??
            throw new KeyNotFoundException("Manufacturer not found.");

        //var category = await _context.Categories.FindAsync(categoryId) ??
        //    throw new KeyNotFoundException("Category not found.");

        var category = await _context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.CategoryId == categoryId) ??
            throw new KeyNotFoundException("Category not found.");

        var random = new Random();
        var randomNumber = random.Next(1000, 9999);
        return $"{manufacturer.Description[..3].ToUpper()}-{category.Description[..2].ToUpper()}-{randomNumber}";
    }
}
