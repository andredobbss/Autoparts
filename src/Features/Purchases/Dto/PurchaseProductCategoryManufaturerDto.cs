using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Purchases.Dto;

public sealed record PurchaseProductCategoryManufaturerDto
(
    string InvoiceNumber,
    int Quantity,
    decimal TotalPurchase,
    EPaymentMethod PaymentMethod,
    string Name,
    string TechnicalDescription,
    string SKU,
    string Compatibility,
    decimal SellingPrice,
    int Stock,
    EStockStatus StockStatus,
    string CategoryDescription,
    string ManufacturerDescription,
    string CompanyName
);
