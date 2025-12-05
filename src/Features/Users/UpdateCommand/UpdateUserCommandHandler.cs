using Autoparts.Api.Features.Users.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.UpdateCommand;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, IdentityResult>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        user.Update(request.UserName,
                    request.Email,
                    request.Password,
                    request.TaxIdType,
                    request.TaxId,
                    request.IsActive,
                    request.Address);

        return await _userRepository.UpdateAsync(user, cancellationToken);
    }
}