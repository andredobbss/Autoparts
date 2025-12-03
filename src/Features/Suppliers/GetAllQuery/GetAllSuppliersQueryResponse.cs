using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQueryResponse(Guid SupplierId,
                                                  string CompanyName,
                                                  string? Email,
                                                  ETaxIdType? TaxIdType,
                                                  string? TaxId,
                                                  DateTime CreatedAt,
                                                  Address Address);
