using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetByIdQuery;

public sealed record GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository) :IRequestHandler<GetSupplierByIdQuery,Supplier>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<Supplier> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
         return await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
    }
}