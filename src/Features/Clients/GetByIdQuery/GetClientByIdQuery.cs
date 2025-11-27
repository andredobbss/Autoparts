using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQuery(Guid ClientId) : IRequest<GetClientByIdQueryResponse>;
