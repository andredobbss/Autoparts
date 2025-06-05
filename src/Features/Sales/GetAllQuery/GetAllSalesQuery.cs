using MediatR;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQuery() : IRequest<GetAllSalesQueryResponse>;
