using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryResponse(Guid PurchaseId,
                                                  string InvoiceNumber,
                                                  EPaymentMethod PaymentMethod,
                                                  decimal TotalPurchase,
                                                  DateTime CreatedAt,
                                                  DateTime UpdatedAt,
                                                  Guid UserId,
                                                  Guid SupplierId,
                                                  IEnumerable<PurchaseProduct> PurchaseProducts,
                                                  IEnumerable<Product> Products);

