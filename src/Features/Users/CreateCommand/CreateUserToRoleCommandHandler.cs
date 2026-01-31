using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateUserToRoleCommandHandler(UserManager<User> userManager) : IRequestHandler<CreateUserToRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateUserToRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        return await userManager.AddToRoleAsync(user, request.RoleName);
    }
}
