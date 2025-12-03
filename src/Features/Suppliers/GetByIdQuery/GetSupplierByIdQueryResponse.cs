using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQueryResponse(Guid SupplierId,
                                                  string CompanyName,
                                                  string? Email,
                                                  ETaxIdType? TaxIdType,
                                                  string? TaxId,
                                                  DateTime CreatedAt,
                                                  Address Address);
