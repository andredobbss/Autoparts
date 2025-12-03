using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryResponse(Guid ClientId,
                                                string ClientName,
                                                string? Email,
                                                ETaxIdType? TaxIdType,
                                                string? EaxId,
                                                DateTime CreatedAt,
                                                Address Address);
