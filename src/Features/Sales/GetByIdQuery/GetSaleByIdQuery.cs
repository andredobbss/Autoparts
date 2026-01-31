using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQuery(Guid SaleId) : IRequest<GetSaleByIdQueryResponse>;
