using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Purchases.Dto;

public sealed record ProductDto(Guid ProductId,
                                string Name,
                                string TechnicalDescription,
                                string SKU,
                                string Compatibility,
                                decimal AcquisitionCost,
                                decimal SellingPrice,
                                int Stock,
                                EStockStatus StockStatus,
                                Guid CategoryId,
                                Guid ManufacturerId);

