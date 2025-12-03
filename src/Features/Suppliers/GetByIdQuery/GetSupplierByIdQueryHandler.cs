using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdQueryResponse>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<GetSupplierByIdQueryResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplierEntity = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
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