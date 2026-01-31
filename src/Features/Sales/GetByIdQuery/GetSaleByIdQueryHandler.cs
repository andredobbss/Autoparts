using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResponse>
{
    public async Task<GetSaleByIdQueryResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await context.Sales!.Include(s => s.User)
                                    .Include(s => s.Client)
                                    .Include(s => s.SaleProducts)
                                    .ThenInclude(sp => sp.Product)
                                    .FirstOrDefaultAsync(s => s.SaleId == request.SaleId, cancellationToken);
        if (sale is null)
            throw new KeyNotFoundException(string.Format(Resource.SALES_NOT_FOUND, request.SaleId));

        return new GetSaleByIdQueryResponse
            (
                sale.SaleId,
                sale.InvoiceNumber,
                sale.TotalSale,
                sale.PaymentMethod,
                sale.DaysLastSale,
                sale.User.UserName!,
                sale.Client.ClientName,
                sale.CreatedAt,
                sale.SaleProducts.Select(sp => new SaleProduct
                (
                    sp.Product.Name,
                    sp.SKU!,
                    sp.Quantity,
                    sp.SellingPrice,
                    sp.TotalItem
                )).ToList()
            );
    }
}