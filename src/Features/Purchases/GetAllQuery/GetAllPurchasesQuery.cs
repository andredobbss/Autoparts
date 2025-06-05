using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed record GetAllPurchasesQuery() : IRequest<GetAllPurchasesQueryResponse>;
