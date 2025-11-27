using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Products.Stock;

public sealed class StockCalculator(AutopartsDbContext context) : IStockCalculator
{
    private readonly AutopartsDbContext _context = context;

    public async Task<bool> StockCalculateAsync(CancellationToken cancellationToken)
    {
        var sql = Resource.UpdateStock;

        int result = await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        return result != 0;
    }
}



