using Autoparts.Api.Features.Clients.Infraestructure;
using MediatR;
namespace Autoparts.Api.Features.Clients.DeleteCommand;
public sealed class DeleteClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);

        if (client is null)
            return false;

        client.Delete();

        var result = await _clientRepository.DeleteAsync(client, cancellationToken);
        return result;
    }
}