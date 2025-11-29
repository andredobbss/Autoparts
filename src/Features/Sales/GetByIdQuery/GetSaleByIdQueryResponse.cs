using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryResponse(Guid SaleId,
                                              string InvoiceNumber,
                                              decimal TotalSale,
                                              EPaymentMethod PaymentMethod,
                                              int DaysLastSale,
                                              Guid UserId,
                                              Guid ClientId,
                                              DateTime CreatedAt,
                                              DateTime? UpdatedAt,
                                              IEnumerable<Product> Products);
