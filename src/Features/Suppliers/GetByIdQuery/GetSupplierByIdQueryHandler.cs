using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetSupplierByIdQueryResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplierEntity = await _context.Suppliers!.FindAsync(request.SupplierId, cancellationToken);
        if (supplierEntity is null)
            throw new KeyNotFoundException(string.Format(Resource.SUPPLIER_NOT_FOUND, request.SupplierId));

        return new GetSupplierByIdQueryResponse
            (
             supplierEntity.SupplierId,
             supplierEntity.CompanyName,
             supplierEntity.Email,
             supplierEntity.TaxIdType,
             supplierEntity.TaxId,
             supplierEntity.CreatedAt,
             supplierEntity.Address
            );
    }
}