using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;

namespace Autoparts.Api.Shared.Products.Stock;

public interface IStockCalculator
{
    Task<ValidationResult> StockCalculateAsync(CancellationToken cancellationToken);
}
