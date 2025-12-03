using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllSalesQueryResponse>>;
