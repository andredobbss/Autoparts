using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeleteCommand;

public sealed record DeleteUserCommand(Guid Id) : IRequest<IdentityResult>;