using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Suppliers.GetAllQuery;

public sealed record GetAllSuppliersQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllSuppliersQuery, PagedResponse<GetAllSuppliersQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllSuppliersQueryResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _context.Suppliers!.AsNoTracking().ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

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