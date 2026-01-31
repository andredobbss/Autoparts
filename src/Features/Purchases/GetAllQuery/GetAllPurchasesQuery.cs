using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllPurchasesQueryResponse>>;
