using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed record CreateRoleCommand(string RoleName) : IRequest<IdentityResult>;

