using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryHandler(ISaleRepository SaleRepository) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResponse>
{
    private readonly ISaleRepository _saleRepository = SaleRepository;
    public async Task<GetSaleByIdQueryResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale is null)
            throw new KeyNotFoundException(string.Format(Resource.SALES_NOT_FOUND, request.SaleId));

        return new GetSaleByIdQueryResponse
            (
                sale.SaleId,
                sale.InvoiceNumber,
                sale.TotalSale,
                sale.PaymentMethod,
                sale.DaysLastSale,
                sale.User.UserName,
                sale.Client.ClientName,
                sale.CreatedAt,
                sale.SaleProducts.Select(sp => new SaleProduct
                (
                    sp.Product.Name,
                    sp.SKU,
                    sp.Quantity,
                    sp.SellingPrice,
                    sp.TotalItem
                )).ToList()
            );
    }
}