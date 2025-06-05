using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQueryHandler():IRequestHandler<GetAllSuppliersQuery,GetAllSuppliersQueryResponse>
{
    public async Task<GetAllSuppliersQueryResponse> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}