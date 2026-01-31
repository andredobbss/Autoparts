using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllSuppliersQueryResponse>>;
