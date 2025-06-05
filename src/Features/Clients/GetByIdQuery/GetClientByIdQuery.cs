using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQuery() : IRequest<GetClientByIdQueryResponse>;
