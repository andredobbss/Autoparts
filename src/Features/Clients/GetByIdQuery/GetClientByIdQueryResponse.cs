using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryResponse(Guid ClientId,
                                                string ClientName,
                                                DateTime CreatedAt,
                                                Address Address);
