using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager) : IRequestHandler<CreateRoleCommand, IdentityResult>
{
    RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
    public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
        if (roleExists)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.ROLE_ALREADY_EXIST, request.RoleName) });

        return await _roleManager.CreateAsync(new IdentityRole<Guid>(request.RoleName));
    }
}
