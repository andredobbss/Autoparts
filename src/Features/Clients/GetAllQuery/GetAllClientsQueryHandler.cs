using MediatR;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryHandler():IRequestHandler<GetAllClientsQuery,GetAllClientsQueryResponse>
{
    public async Task<GetAllClientsQueryResponse> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}