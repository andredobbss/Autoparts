using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetReturnByIdQuery, GetReturnByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetReturnByIdQueryResponse> Handle(GetReturnByIdQuery request, CancellationToken cancellationToken)
    {
        var returnEntity = await _context.Returns!.Include(r => r.User)
                                      .Include(r => r.Client)
                                      .Include(r => r.ReturnProducts)
                                      .ThenInclude(rp => rp.Product)
                                      .FirstOrDefaultAsync(r => r.ReturnId == request.ReturnId, cancellationToken);
        if (returnEntity is null)
            throw new KeyNotFoundException(string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId));

        return new GetReturnByIdQueryResponse
        (
            returnEntity.ReturnId,
            returnEntity.Justification,
            returnEntity.InvoiceNumber,
            returnEntity.CreatedAt,
            returnEntity.User.UserName!,
            returnEntity.Client.ClientName,
            returnEntity.ReturnProducts.Select(rp => new ReturnProduct
            (
                rp.Product.Name,
                rp.Product.SKU,
                rp.Quantity,
                rp.SellingPrice,
                rp.TotalItem,
                rp.Loss
            )).ToList()
        );
    }
}