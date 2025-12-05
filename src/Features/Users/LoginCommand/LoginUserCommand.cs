using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.LoginCommand;

public record LoginUserCommand(string UserName, string Password) : IRequest<SignInResult>;

