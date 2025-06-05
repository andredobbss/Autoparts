using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryHandler():IRequestHandler<GetManufacturerByIdQuery,GetManufacturerByIdQueryResponse>
{
    public async Task<GetManufacturerByIdQueryResponse> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}