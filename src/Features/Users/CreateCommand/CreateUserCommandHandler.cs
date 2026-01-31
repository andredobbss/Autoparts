using Autoparts.Api.Features.Users.Domain;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateUserCommandHandler(UserManager<User> _userManager) : IRequestHandler<CreateUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        (
           request.UserName,
           request.Email,
           request.Password,
           request.TaxIdType,
           request.TaxId,
           request.Address
        );

        return await _userManager.CreateAsync(user, request.Password);
    }
}