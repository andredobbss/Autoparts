namespace Autoparts.Api.Shared.Services;

public interface ISkuGenerator
{
    Task<string> GenerateSKUAsync(Guid manufacturerId, Guid categoryId, CancellationToken cancellationToken);
}
