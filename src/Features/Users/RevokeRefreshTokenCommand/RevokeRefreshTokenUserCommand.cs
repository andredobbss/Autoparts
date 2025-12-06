using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.RevokeCommand;

public sealed record RevokeRefreshTokenUserCommand(Guid Id) : IRequest<IdentityResult>;
