using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetAllSuppliersQuery, PagedResponse<GetAllSuppliersQueryResponse>>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<PagedResponse<GetAllSuppliersQueryResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = suppliers
            .Select(s => new GetAllSuppliersQueryResponse
        (
            s.SupplierId,
            s.CompanyName,
            s.Email,
            s.TaxIdType,
            s.TaxId,
            s.CreatedAt,
            s.Address
        ));

        return new PagedResponse<GetAllSuppliersQueryResponse>(pagedResponse);
    }
}