namespace Autoparts.Api.Features.Purchases.Dto;

public sealed record PurchaseProductDto(Guid ProductId,
                                        int Quantity,
                                        decimal SellingPrice,
                                        decimal TotalItem,
                                        ProductDto Product);

