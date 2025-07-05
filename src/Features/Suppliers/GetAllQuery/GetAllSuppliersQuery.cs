using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Supplier>>;
