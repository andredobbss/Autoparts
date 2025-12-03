using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryResponse(Guid PurchaseId,
                                                  string InvoiceNumber,
                                                  EPaymentMethod PaymentMethod,
                                                  decimal TotalPurchase,
                                                  DateTime CreatedAt,
                                                  string UserName,
                                                  string SupplierName,
                                                  IEnumerable<PurchaseProduct> PurchaseProducts);
