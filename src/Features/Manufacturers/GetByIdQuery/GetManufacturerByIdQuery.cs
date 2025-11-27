using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQuery(Guid ManufacturerId) : IRequest<GetManufacturerByIdQueryResponse>;
