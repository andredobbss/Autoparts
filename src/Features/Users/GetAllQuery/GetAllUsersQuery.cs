using MediatR;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQuery() : IRequest<IEnumerable<GetAllUsersQueryResponse>>;
