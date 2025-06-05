using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQuery() : IRequest<GetAllSuppliersQueryResponse>;
