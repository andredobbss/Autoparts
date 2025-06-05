using MediatR;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQuery() : IRequest<GetAllProductsQueryResponse>;
