using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQueryHandler():IRequestHandler<GetAllPurchasesQuery,GetAllPurchasesQueryResponse>
{
    public async Task<GetAllPurchasesQueryResponse> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}