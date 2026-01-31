using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager) : IRequestHandler<CreateRoleCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExists = await roleManager.RoleExistsAsync(request.RoleName);
        if (roleExists)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.ROLE_ALREADY_EXIST, request.RoleName) });

        return await roleManager.CreateAsync(new IdentityRole<Guid>(request.RoleName));
    }
}
