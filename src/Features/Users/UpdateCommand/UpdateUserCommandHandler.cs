using MediatR;
namespace Autoparts.Api.Features.Users.UpdateCommand;
public sealed class UpdateUserCommandHandler():IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}