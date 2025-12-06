using Autoparts.Api.Features.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.LogoutCommand;

public sealed class LogoutUserCommandHandler(SignInManager<User> signin) : IRequestHandler<LogoutUserCommand, SignInResult>
{
    SignInManager<User> _signin = signin;
    public async Task<SignInResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signin.SignOutAsync();

        return SignInResult.Success;
    }
}
