using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Reports.DTOs;

public sealed record ProductsList(
    string Name,
    decimal SellingPrice,
    decimal AcquisitionCost,
    decimal Margin,
    int Stock,
    string? InvoiceNumber,
    EPaymentMethod? PaymentMethod,
    int? Quantity,
    decimal? TotalItem,
    decimal? TotalSale,
    DateTime? CreatedAt
)
{
    public EStockStatusOverTime StockStatusOverTime =>
        CreatedAt.HasValue && (DateTime.UtcNow - CreatedAt.Value).Days > 90
            ? EStockStatusOverTime.StagnantStock
            : EStockStatusOverTime.Active;
}
