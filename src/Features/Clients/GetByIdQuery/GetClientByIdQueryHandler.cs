using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientByIdQuery, Client>
{
    readonly IClientRepository _clientRepository = clientRepository;
    public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);

    }
}