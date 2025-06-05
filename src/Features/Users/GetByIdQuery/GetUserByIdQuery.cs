using MediatR;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQuery() : IRequest<GetUserByIdQueryResponse>;
