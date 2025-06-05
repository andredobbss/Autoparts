using MediatR;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQueryHandler():IRequestHandler<GetAllProductsQuery,GetAllProductsQueryResponse>
{
    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}