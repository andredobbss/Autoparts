using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeleteCommand;

public sealed class DeleteUserCommandHandler(UserManager<User> userManager) : IRequestHandler<DeleteUserCommand, IdentityResult>
{
    private readonly UserManager<User> _userManager = userManager;
    public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        return await _userManager.DeleteAsync(user);
    }
}