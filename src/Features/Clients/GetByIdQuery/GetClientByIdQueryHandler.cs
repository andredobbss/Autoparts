using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResponse>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<GetClientByIdQueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
        if (client is null)
            throw new KeyNotFoundException(string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId));

        return new GetClientByIdQueryResponse
        (
             client.ClientId,
             client.ClientName,
             client.Email,
             client.TaxIdType,
             client.TaxId,
             client.CreatedAt,
             client.Address
        );

    }
}