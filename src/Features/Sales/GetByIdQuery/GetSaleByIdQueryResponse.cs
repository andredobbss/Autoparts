using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Enums;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryResponse(Guid SaleId,
                                              string InvoiceNumber,
                                              decimal TotalSale,
                                              EPaymentMethod PaymentMethod,
                                              int DaysLastSale,
                                              string UserName,
                                              string ClientName,
                                              DateTime CreatedAt,
                                              IEnumerable<SaleProduct> SaleProducts);
