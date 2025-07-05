using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Products.Stock;

public static class StockDictionary
{
    public static async Task<IDictionary<Guid, int>> GetStockDictionary(AutopartsDbContext _context, CancellationToken cancellationToken)
    {
        var purchaseProducts = await _context.PurchaseProducts
            .GroupBy(pp => pp.ProductId)
            .Select(g => new { ProductId = g.Key, Quantity = g.Sum(pp => pp.Quantity) })
            .ToListAsync(cancellationToken);

        var returnProducts = await _context.ReturnProducts
            .GroupBy(rp => rp.ProductId)
            .Select(g => new { ProductId = g.Key, Quantity = g.Sum(rp => rp.Quantity) })
            .ToListAsync(cancellationToken);

        var saleProducts = await _context.SaleProducts
            .GroupBy(sp => sp.ProductId)
            .Select(g => new { ProductId = g.Key, Quantity = g.Sum(sp => sp.Quantity) })
            .ToListAsync(cancellationToken);

        IDictionary<Guid, int> stockDictionary = new Dictionary<Guid, int>();

        if (purchaseProducts.Count() > 0)
            foreach (var purchaseProduct in purchaseProducts)
                stockDictionary.Add(purchaseProduct.ProductId, purchaseProduct.Quantity);

        if (returnProducts.Count() > 0)
            foreach (var returnProduct in returnProducts)
                stockDictionary.Add(returnProduct.ProductId, returnProduct.Quantity);

        if (saleProducts.Count() > 0)
            foreach (var saleProduct in saleProducts)
                stockDictionary.Add(saleProduct.ProductId, saleProduct.Quantity);

        return await Task.FromResult(stockDictionary);

    }
}
