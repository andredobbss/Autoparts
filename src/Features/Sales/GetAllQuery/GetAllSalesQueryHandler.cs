using MediatR;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQueryHandler():IRequestHandler<GetAllSalesQuery,GetAllSalesQueryResponse>
{
    public async Task<GetAllSalesQueryResponse> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}