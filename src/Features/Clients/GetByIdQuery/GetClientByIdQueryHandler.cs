using AutoMapper;
using Autoparts.Api.Features.Clients.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper) : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResponse>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetClientByIdQueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);

        return _mapper.Map<GetClientByIdQueryResponse>(client);

    }
}