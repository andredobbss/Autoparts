using Autoparts.Api.Features.Users.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.LoginCommand;

public class LoginUserCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginUserCommand, SignInResult>
{
    private readonly IUserRepository _userRepository = userRepository;
    public Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
       return _userRepository.LoginAsync(request.UserName, request.Password, cancellationToken);
    }
}
