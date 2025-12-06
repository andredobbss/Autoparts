using Autoparts.Api.Features.Users.DTOs;
using MediatR;

namespace Autoparts.Api.Features.Users.LoginCommand;

public sealed record LoginUserCommand(string UserName, string Password) : IRequest<Token>;

