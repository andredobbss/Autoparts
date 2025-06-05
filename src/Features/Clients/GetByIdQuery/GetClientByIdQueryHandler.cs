using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler():IRequestHandler<GetClientByIdQuery,GetClientByIdQueryResponse>
{
    public async Task<GetClientByIdQueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}