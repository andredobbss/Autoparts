using Autoparts.Api.Shared.Paginate;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllClientsQueryResponse>>;
