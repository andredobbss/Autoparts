namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUserRolesQueryResponse(string UserName, IEnumerable<string> Roles);

