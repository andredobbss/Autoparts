using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly IUserRepository _userRepository = userRepository;
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

        return await _userRepository.AddAsync(user, cancellationToken);
    }
}