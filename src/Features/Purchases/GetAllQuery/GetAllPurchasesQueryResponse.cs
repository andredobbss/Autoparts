using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryResponse(Guid PurchaseId,
                                                  string InvoiceNumber,
                                                  EPaymentMethod PaymentMethod,
                                                  decimal TotalPurchase,
                                                  DateTime CreatedAt,
                                                  string UserName,
                                                  string SupplierName,
                                                  IEnumerable<ProductDto> Products);

