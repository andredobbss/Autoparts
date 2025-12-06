using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Purchases.GetByIdQuery;

public sealed record GetPurchaseByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetPurchaseByIdQuery, GetPurchaseByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetPurchaseByIdQueryResponse> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases!.Include(p => p.User)
                                                .Include(p => p.Supplier)
                                                .Include(P => P.PurchaseProducts)
                                                .ThenInclude(pp => pp.Product)
                                                .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);
        if (purchase is null)
            throw new KeyNotFoundException(string.Format(Resource.PURCHASE_NOT_FOUND, request.PurchaseId));

        return new GetPurchaseByIdQueryResponse
            (
                purchase!.PurchaseId,
                purchase.InvoiceNumber,
                purchase.PaymentMethod,
                purchase.TotalPurchase,
                purchase.CreatedAt,
                purchase.User.UserName!,
                purchase.Supplier.CompanyName,
                purchase.PurchaseProducts.Select(pp => new PurchaseProduct
                (
                    pp.Product.Name,
                    pp.Product.SKU,
                    pp.Quantity,
                    pp.AcquisitionCost,
                    pp.TotalItem
                )).ToList()
            );
    }
}