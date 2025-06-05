using MediatR;
namespace Autoparts.Api.Features.Users.DeleteCommand;
public sealed class DeleteUserCommandHandler():IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}