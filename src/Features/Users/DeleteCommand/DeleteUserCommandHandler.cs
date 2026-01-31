using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeleteCommand;

public sealed class DeleteUserCommandHandler(UserManager<User> _userManager) : IRequestHandler<DeleteUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        return await _userManager.DeleteAsync(user);
    }
}