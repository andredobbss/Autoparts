using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetAllClientsQuery, PagedResponse<GetAllClientsQueryResponse>>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<PagedResponse<GetAllClientsQueryResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = clients.
            Select(c => new GetAllClientsQueryResponse
            (
                c.ClientId,
                c.ClientName,
                c.Email,
                c.TaxIdType,
                c.TaxId,
                c.CreatedAt,
                c.Address
            ));

        return new PagedResponse<GetAllClientsQueryResponse>(pagedResponse);
    }
}