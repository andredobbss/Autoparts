using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Product>>;
