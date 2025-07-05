namespace Autoparts.Api.Features.Products.Infraestructure;

public interface ISkuGenerator
{
    Task<string> GenerateSKUAsync(Guid manufacturerId, Guid categoryId, CancellationToken cancellationToken);
}
