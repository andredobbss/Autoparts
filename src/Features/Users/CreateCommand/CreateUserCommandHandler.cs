using MediatR;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed class CreateUserCommandHandler() : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}