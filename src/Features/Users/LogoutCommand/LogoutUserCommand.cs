using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.LogoutCommand;

public sealed record LogoutUserCommand() : IRequest<SignInResult>;

