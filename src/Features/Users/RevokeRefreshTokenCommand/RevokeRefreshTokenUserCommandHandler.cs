using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.RevokeCommand;

public sealed class RevokeRefreshTokenUserCommandHandler(UserManager<User> userManager) : IRequestHandler<RevokeRefreshTokenUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RevokeRefreshTokenUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        user.RevokeRefreshToken();

        return await userManager.UpdateAsync(user);
    }
}
