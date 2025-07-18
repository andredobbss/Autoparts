using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.Dto;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Shared.Products.Stock;

public sealed class StockCalculator(AutopartsDbContext context) : IStockCalculator
{
    private readonly AutopartsDbContext _context = context;

    public async Task<ValidationResult> StockCalculateAsync(CancellationToken cancellationToken)
    {
        ValidationResult productResult = new();

        #region old commented code

        //List<Product> updatedProducts = [];

        //var products = _context.Products.AsNoTracking();

        //var stockPurchase = await _context.PurchaseProducts
        //    .GroupBy(pp => pp.ProductId)
        //    .Select(g => new { ProductId = g.Key, Quantity = g.Sum(pp => pp.Quantity) })
        //    .ToDictionaryAsync(pp => pp.ProductId, pp => pp.Quantity, cancellationToken);

        //var stockReturn = await _context.ReturnProducts
        //    .GroupBy(pp => pp.ProductId)
        //    .Select(g => new { ProductId = g.Key, Quantity = g.Sum(pp => pp.Quantity) })
        //    .ToDictionaryAsync(pp => pp.ProductId, pp => pp.Quantity, cancellationToken);

        //var stockSale = await _context.SaleProducts
        //    .GroupBy(pp => pp.ProductId)
        //    .Select(g => new { ProductId = g.Key, Quantity = g.Sum(pp => pp.Quantity) })
        //    .ToDictionaryAsync(pp => pp.ProductId, pp => pp.Quantity, cancellationToken);

        //foreach (var product in products)
        //{
        //    stockPurchaseValue = stockPurchase.TryGetValue(product.ProductId, out int purchaseValue) ? purchaseValue : 0;
        //    stockReturnValue = stockReturn.TryGetValue(product.ProductId, out int returnValue) ? returnValue : 0;
        //    stockSaleValue = stockSale.TryGetValue(product.ProductId, out int saleValue) ? saleValue : 0;

        //    stock = stockPurchaseValue + stockReturnValue - stockSaleValue;

        //    product.SetStock(stock);

        //    updatedProducts.Add(product);
        //}

        //_context.Products.UpdateRange(updatedProducts);

        //var commitResult = await _context.SaveChangesAsync(cancellationToken);
        //if (commitResult <= 0)
        //    return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        #endregion

        #region new code

        //List<Product> updatedProducts = [];

        //var products = _context.Products.AsNoTracking();

        //var stockMovements = await (from pp in _context.PurchaseProducts
        //                            select new { pp.ProductId, pp.Quantity, Type = "Purchase" })
        //                                   .Union(
        //                                       from rp in _context.ReturnProducts
        //                                       select new { rp.ProductId, rp.Quantity, Type = "Return" })
        //                                   .Union(
        //                                       from sp in _context.SaleProducts
        //                                       select new { sp.ProductId, sp.Quantity, Type = "Sale" })
        //                                   .ToListAsync(cancellationToken);

        //var stockByProduct = stockMovements
        //                    .GroupBy(m => m.ProductId)
        //                    .ToDictionary(
        //                        g => g.Key,
        //                        g =>
        //                        {
        //                            int purchaseTotal = g.Where(x => x.Type == "Purchase").Sum(x => x.Quantity);
        //                            int returnTotal = g.Where(x => x.Type == "Return").Sum(x => x.Quantity);
        //                            int saleTotal = g.Where(x => x.Type == "Sale").Sum(x => x.Quantity);

        //                            return purchaseTotal + returnTotal - saleTotal;
        //                        });


        //foreach (var product in products)
        //{
        //    int stock = stockByProduct.TryGetValue(product.ProductId, out int stockValue) ? stockValue : 0;
        //    product.SetStock(stock);
        //    updatedProducts.Add(product);
        //}

        //_context.Products.UpdateRange(updatedProducts);

        //var commitResult = await _context.SaveChangesAsync(cancellationToken);
        //if (commitResult <= 0)
        //    return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        #endregion

        #region commented code

        //List<Product> updatedProducts = [];

        //var products = _context.Products.AsNoTracking();

        //var stockList = await _context.Database
        //    .SqlQuery<StockProjectionDto>($@"SELECT ProductId, SUM(Quantity) AS Stock
        //                                     FROM (
        //                                         SELECT ProductId, Quantity FROM PurchaseProducts
        //                                         UNION ALL
        //                                         SELECT ProductId, Quantity FROM ReturnProducts
        //                                         UNION ALL
        //                                         SELECT ProductId, -Quantity AS Quantity FROM SaleProducts
        //                                     ) AS StockMovements
        //                                     GROUP BY ProductId;").ToListAsync(cancellationToken); 



        //foreach (var product in products)
        //{
        //    var stock = stockList.FirstOrDefault(s => s.ProductId == product.ProductId)?.Stock ?? 0;
        //    product.SetStock(stock);
        //    updatedProducts.Add(product);
        //}

        //_context.Products.UpdateRange(updatedProducts);

        //var commitResult = await _context.SaveChangesAsync(cancellationToken);
        //if (commitResult <= 0)
        //    return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        #endregion

        var sql = Resource.UpdateStock;

        int result = await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        if (result == 0)
            return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        return productResult;
    }
}



