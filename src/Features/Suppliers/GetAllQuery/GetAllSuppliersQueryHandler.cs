using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetAllSuppliersQuery, PagedResponse<Supplier>>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<PagedResponse<Supplier>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new PagedResponse<Supplier>(suppliers);
    }
}