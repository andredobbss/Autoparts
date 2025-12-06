namespace Autoparts.Api.Shared.Services;

public interface IStockCalculator
{
    Task<bool> StockCalculateAsync(CancellationToken cancellationToken);
}
