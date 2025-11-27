using AutoMapper;
using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper) : IRequestHandler<GetAllClientsQuery, PagedResponse<GetAllClientsQueryResponse>>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllClientsQueryResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var clientsResponse = _mapper.Map<IEnumerable<GetAllClientsQueryResponse>>(clients.ToList());

        var pagedResponse = new StaticPagedList<GetAllClientsQueryResponse>(clientsResponse, clients.GetMetaData());

        return new PagedResponse<GetAllClientsQueryResponse>(pagedResponse);
    }
}