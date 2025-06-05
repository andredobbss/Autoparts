using MediatR;
namespace Autoparts.Api.Features.Clients.UpdateCommand;
public sealed class UpdateClientCommandHandler():IRequestHandler<UpdateClientCommand>
{
    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}