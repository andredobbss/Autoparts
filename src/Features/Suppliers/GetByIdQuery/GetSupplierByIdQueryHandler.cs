using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQueryHandler():IRequestHandler<GetSupplierByIdQuery,GetSupplierByIdQueryResponse>
{
    public async Task<GetSupplierByIdQueryResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}