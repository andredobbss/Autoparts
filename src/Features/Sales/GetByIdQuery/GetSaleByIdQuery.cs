using MediatR;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQuery() : IRequest<GetSaleByIdQueryResponse>;
