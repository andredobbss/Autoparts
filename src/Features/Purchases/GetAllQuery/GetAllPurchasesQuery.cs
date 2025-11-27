using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

//public sealed record GetAllPurchasesQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetAllPurchasesQueryResponse>>;
public sealed record GetAllPurchasesQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Purchase>>;
