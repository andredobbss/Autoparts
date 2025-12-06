using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.RevokeCommand;

public sealed class RevokeRefreshTokenUserCommandHandler(UserManager<User> userManager) : IRequestHandler<RevokeRefreshTokenUserCommand, IdentityResult>
{
    private readonly UserManager<User> _userManager = userManager;
    public async Task<IdentityResult> Handle(RevokeRefreshTokenUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        user.RevokeRefreshToken();

        return await _userManager.UpdateAsync(user);
    }
}
