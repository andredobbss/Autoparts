using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQuery(Guid PurchaseId) : IRequest<GetPurchaseByIdQueryResponse>;
