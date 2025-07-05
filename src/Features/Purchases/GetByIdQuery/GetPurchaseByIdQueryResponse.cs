using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQueryResponse(Guid PurchaseId,
                                                  string InvoiceNumber,
                                                  EPaymentMethod PaymentMethod,
                                                  decimal TotalPurchase,
                                                  DateTime CreatedAt,
                                                  Guid UserId,
                                                  Guid SupplierId,
                                                  IEnumerable<PurchaseProductDto> PurchaseProducts);
