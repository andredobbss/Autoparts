using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQuery(Guid SupplierId) : IRequest<GetSupplierByIdQueryResponse>;
