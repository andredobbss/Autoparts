using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryResponse(Guid ClientId,
                                                string ClientName,
                                                string? Email,
                                                ETaxIdType? TaxIdType,
                                                string? EaxId,
                                                DateTime CreatedAt,
                                                Address Address);
