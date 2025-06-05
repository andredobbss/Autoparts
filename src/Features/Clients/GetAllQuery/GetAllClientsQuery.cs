using MediatR;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQuery() : IRequest<GetAllClientsQueryResponse>;
