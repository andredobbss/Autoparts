using Autoparts.Api.Features.Products.Domain;
using MediatR;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<Product>;
