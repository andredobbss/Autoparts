using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQuery() : IRequest<IEnumerable<GetAllUsersQueryResponse>>;
