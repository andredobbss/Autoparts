using MediatR;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQuery() : IRequest<GetProductByIdQueryResponse>;
