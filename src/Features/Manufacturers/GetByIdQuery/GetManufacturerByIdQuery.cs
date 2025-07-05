using Autoparts.Api.Features.Manufacturers.Domain;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQuery(Guid ManufacturerId) : IRequest<Manufacturer>;
