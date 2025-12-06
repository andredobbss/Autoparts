using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.UpdateCommand;

public sealed class UpdateUserCommandHandler(UserManager<User> userManager) : IRequestHandler<UpdateUserCommand, IdentityResult>
{
    private readonly UserManager<User> _userManager = userManager;
    public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        user.Update(request.UserName,
                    request.Email,
                    request.Password,
                    request.TaxIdType,
                    request.TaxId,
                    request.Address);

        return await _userManager.UpdateAsync(user);
    }
}