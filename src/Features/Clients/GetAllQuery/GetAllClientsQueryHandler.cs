using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetAllClientsQuery, PagedResponse<Client>>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<PagedResponse<Client>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
       var clients = await _clientRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
       return new PagedResponse<Client>(clients);
    }
}