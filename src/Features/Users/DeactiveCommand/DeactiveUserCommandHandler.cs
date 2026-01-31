using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeactiveCommand;

public sealed class DeactiveUserCommandHandler(UserManager<User> _userManager) : IRequestHandler<DeactiveUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(DeactiveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        user.Deactivate();

        return await _userManager.UpdateAsync(user);
    }
}
