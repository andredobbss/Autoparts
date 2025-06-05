using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQuery() : IRequest<GetAllManufacturersQueryResponse>;
