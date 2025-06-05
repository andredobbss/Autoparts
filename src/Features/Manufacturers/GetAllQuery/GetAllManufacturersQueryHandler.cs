using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryHandler():IRequestHandler<GetAllManufacturersQuery,GetAllManufacturersQueryResponse>
{
    public async Task<GetAllManufacturersQueryResponse> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}