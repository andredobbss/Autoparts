using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;

namespace Autoparts.Api.Shared.Products.Stock;

public sealed class StockCalculator(AutopartsDbContext context) : IStockCalculator
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> AddCalculateStockAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
    {
        ValidationResult productResult = new();

        List<Product> updatedProducts = [];

        var totalStockDictionary = await StockDictionary.GetStockDictionary(_context, cancellationToken);

        foreach (var product in products)
        {
            int stock = totalStockDictionary.Where(key => key.Key == product.ProductId).Sum(key => key.Value);

            stock += product.Quantity;

            product.SetStock(stock);

            updatedProducts.Add(product);
        }

        _context.Products.UpdateRange(updatedProducts);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
            return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        return productResult;
    }


    public async Task<ValidationResult> UpdateCalculateStockAsync(IEnumerable<Product> products, IDictionary<Guid, int> idAndQuantityDictionary, Guid id, CancellationToken cancellationToken)
    {
        ValidationResult productResult = new();

        List<Product> updatedProducts = [];

        var totalStockDictionary = await StockDictionary.GetStockDictionary(_context, cancellationToken);

        foreach (var product in products)
        {
            int stock = totalStockDictionary.Where(key => key.Key == product.ProductId).Sum(key => key.Value);

            int sumQuantity = idAndQuantityDictionary.Where(key => key.Key == product.ProductId).Sum(key => key.Value);

            stock = stock - sumQuantity + product.Quantity;

            product.SetStock(stock);

            updatedProducts.Add(product);
        }

        _context.Products.UpdateRange(updatedProducts);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
            return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        return productResult;
    }

    public async Task<ValidationResult> DeleteCalculateStockAsync(IEnumerable<Product> products, Guid id, CancellationToken cancellationToken)
    {
        ValidationResult productResult = new();

        List<Product> updatedProducts = [];

        var totalStockDictionary = await StockDictionary.GetStockDictionary(_context, cancellationToken);

        foreach (var product in products)
        {
            int stock = totalStockDictionary.Where(key => key.Key == product.ProductId).Sum(key => key.Value);
         
            stock -= product.Quantity;
          
            product.SetStock(stock);

            updatedProducts.Add(product);
        }

        _context.Products.UpdateRange(updatedProducts);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
            return productResult = new ValidationResult { Errors = { new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE) } };

        return productResult;
    }
}

