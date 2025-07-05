using Autoparts.Api.Features.Products.Domain;
using FluentValidation.Results;

namespace Autoparts.Api.Shared.Products.Stock;

public interface IStockCalculator
{
    Task<ValidationResult> AddCalculateStockAsync(IEnumerable<Product> products, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateCalculateStockAsync(IEnumerable<Product> products, IDictionary<Guid, int> idAndQuantityDictionary, Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> DeleteCalculateStockAsync(IEnumerable<Product> products, Guid id, CancellationToken cancellationToken);
}
