namespace Autoparts.Api.Shared.Products.Stock;

public interface IStockCalculator
{
    Task<bool> StockCalculateAsync(CancellationToken cancellationToken);
}
