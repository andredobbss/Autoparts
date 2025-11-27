using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllManufacturersQueryResponse>>;
