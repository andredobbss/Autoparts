using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryResponse(Guid ClientId,
                                                string ClientName,
                                                DateTime CreatedAt,
                                                Address Address);
