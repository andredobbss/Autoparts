using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Services;

public sealed class StockCalculator(AutopartsDbContext context) : IStockCalculator
{
    private readonly AutopartsDbContext _context = context;

    public async Task<bool> StockCalculateAsync(CancellationToken cancellationToken)
    {
        const string sql = @"UPDATE Products
                             SET Stock = StockMovements.Stock
                             FROM (
                                 SELECT 
                                     ProductId,
                                     SUM(Quantity) AS Stock
                                 FROM (
                                     SELECT ProductId, Quantity FROM PurchaseProducts
                                     UNION ALL
                                     SELECT ProductId, Quantity FROM ReturnProducts WHERE Loss = 0
                                     UNION ALL
                                     SELECT ProductId, -Quantity AS Quantity FROM SaleProducts
                                 ) AS Movements
                                 GROUP BY ProductId
                             ) AS StockMovements
                             WHERE Products.ProductId = StockMovements.ProductId";

        int result = await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        return result != 0;
    }
}



