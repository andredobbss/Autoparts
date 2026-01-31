using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllProductsQueryResponse>>;
