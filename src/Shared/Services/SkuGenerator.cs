using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;

namespace Autoparts.Api.Shared.Services;

internal sealed class SkuGenerator(AutopartsDbContext context) : ISkuGenerator
{
    public async Task<string> GenerateSKUAsync(Guid manufacturerId, Guid categoryId, CancellationToken cancellationToken)
    {
        var manufacturer = await context.Manufacturers!.FindAsync(manufacturerId, cancellationToken) ??
            throw new KeyNotFoundException(string.Format(Resource.MANUFACTORER_NOT_FOUND, manufacturerId));

        var category = await context.Categories!.FindAsync(categoryId, cancellationToken) ??
            throw new KeyNotFoundException(string.Format(Resource.PRODUCTS_NOT_FOUND, categoryId));

        var random = new Random();
        var randomNumber = random.Next(1000, 9999);
        return $"{manufacturer.Description[..3].ToUpper()}-{category.Description[..2].ToUpper()}-{randomNumber}";
    }
}
