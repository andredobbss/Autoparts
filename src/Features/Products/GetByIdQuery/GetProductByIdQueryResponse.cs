using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQueryResponse(Guid ProductId, string Name, string TechnicalDescription, string SKU, string Compatibility, decimal AcquisitionCost, decimal SellingPrice, int Quantity, int Stock, EStockStatus StockStatus, DateTime CreatedAt, DateTime UpdatedAt);
