using Autoparts.Api.Features.Users.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.DeleteCommand;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, IdentityResult>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Description = string.Format(Resource.USER_NOT_FOUND, request.Id) });

        return await _userRepository.DeleteAsync(user, cancellationToken);
    }
}