using MediatR;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQueryHandler():IRequestHandler<GetPurchaseByIdQuery,GetPurchaseByIdQueryResponse>
{
    public async Task<GetPurchaseByIdQueryResponse> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}