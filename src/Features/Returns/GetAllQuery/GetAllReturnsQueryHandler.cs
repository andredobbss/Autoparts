using MediatR;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryHandler():IRequestHandler<GetAllReturnsQuery,GetAllReturnsQueryResponse>
{
    public async Task<GetAllReturnsQueryResponse> Handle(GetAllReturnsQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}