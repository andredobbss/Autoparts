using MediatR;
namespace Autoparts.Api.Features.Clients.CreateCommand;
public sealed class CreateClientCommandHandler :IRequestHandler<CreateClientCommand>
{
    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}