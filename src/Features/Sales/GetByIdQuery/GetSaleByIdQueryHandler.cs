using MediatR;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryHandler():IRequestHandler<GetSaleByIdQuery,GetSaleByIdQueryResponse>
{
    public async Task<GetSaleByIdQueryResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}