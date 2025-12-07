using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed record CreateUserToRoleCommand(Guid Id, string RoleName) : IRequest<IdentityResult>;

