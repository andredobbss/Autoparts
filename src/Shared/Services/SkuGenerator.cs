using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;

namespace Autoparts.Api.Shared.Services;

internal sealed class SkuGenerator : ISkuGenerator
{
    private readonly AutopartsDbContext _context;

    public SkuGenerator(AutopartsDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateSKUAsync(Guid manufacturerId, Guid categoryId, CancellationToken cancellationToken)
    {
        var manufacturer = await _context.Manufacturers!.FindAsync(manufacturerId, cancellationToken) ??
            throw new KeyNotFoundException(string.Format(Resource.MANUFACTORER_NOT_FOUND, manufacturerId));

        var category = await _context.Categories!.FindAsync(categoryId, cancellationToken) ??
            throw new KeyNotFoundException(string.Format(Resource.PRODUCTS_NOT_FOUND, categoryId));

        var random = new Random();
        var randomNumber = random.Next(1000, 9999);
        return $"{manufacturer.Description[..3].ToUpper()}-{category.Description[..2].ToUpper()}-{randomNumber}";
    }
}
