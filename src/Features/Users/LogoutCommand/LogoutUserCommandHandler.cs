using Autoparts.Api.Features.Users.Domain;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.LogoutCommand;

public sealed class LogoutUserCommandHandler(SignInManager<User> _signin) : IRequestHandler<LogoutUserCommand, SignInResult>
{
    public async Task<SignInResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signin.SignOutAsync();

        return SignInResult.Success;
    }
}
