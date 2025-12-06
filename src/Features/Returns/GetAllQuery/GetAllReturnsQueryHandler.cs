using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryHandler(AutopartsDbContext context) : IRequestHandler<GetAllReturnsQuery, PagedResponse<GetAllReturnsQueryResponse>>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<PagedResponse<GetAllReturnsQueryResponse>> Handle(GetAllReturnsQuery request, CancellationToken cancellationToken)
    {
        var returns = await _context.Returns!.AsNoTracking()
                                             .Include(r => r.User)
                                             .Include(r => r.Client)
                                             .Include(r => r.ReturnProducts)
                                             .ThenInclude(rp => rp.Product)
                                             .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);


        var pagedResponse = returns
            .Select(r => new GetAllReturnsQueryResponse
        (
            r.ReturnId,
            r.Justification,
            r.InvoiceNumber,
            r.CreatedAt,
            r.User.UserName!,
            r.Client.ClientName,
            r.ReturnProducts.Select(rp => new ReturnProduct
            (
                rp.Product.Name,
                rp.Product.SKU,
                rp.Quantity,
                rp.SellingPrice,
                rp.TotalItem,
                rp.Loss
            )).ToList()
        ));

        return new PagedResponse<GetAllReturnsQueryResponse>(pagedResponse);
    }
}