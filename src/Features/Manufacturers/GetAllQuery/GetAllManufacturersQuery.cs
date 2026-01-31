using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllManufacturersQueryResponse>>;
