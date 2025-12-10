using MediatR;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUserRolesQuery() : IRequest<IEnumerable<GetAllUserRolesQueryResponse>>;