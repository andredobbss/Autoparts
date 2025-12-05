using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeactiveCommand;

public sealed record DeactiveUserCommand(Guid Id) : IRequest<IdentityResult>;

