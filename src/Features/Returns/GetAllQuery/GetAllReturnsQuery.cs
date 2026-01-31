using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllReturnsQueryResponse>>;
