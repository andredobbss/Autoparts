using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllClientsQuery, PagedResponse<GetAllClientsQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllClientsQueryResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _context.Clients!.AsNoTracking()
                                             .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = clients.
            Select(c => new GetAllClientsQueryResponse
            (
                c.ClientId,
                c.ClientName,
                c.Email,
                c.TaxIdType,
                c.TaxId,
                c.CreatedAt,
                c.Address
            ));

        return new PagedResponse<GetAllClientsQueryResponse>(pagedResponse);
    }
}