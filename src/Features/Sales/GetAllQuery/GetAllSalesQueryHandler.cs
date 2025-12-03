using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQueryHandler(ISaleRepository SaleRepository) : IRequestHandler<GetAllSalesQuery, PagedResponse<GetAllSalesQueryResponse>>
{
    private readonly ISaleRepository _saleRepository = SaleRepository;
    public async Task<PagedResponse<GetAllSalesQueryResponse>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = sales
            .Select(s => new GetAllSalesQueryResponse
            (
                s.SaleId,
                s.InvoiceNumber,
                s.TotalSale,
                s.PaymentMethod,
                s.DaysLastSale,
                s.User.UserName,
                s.Client.ClientName,
                s.CreatedAt,
                s.SaleProducts.Select(sp => new SaleProduct
                (
                    sp.Product.Name,
                    sp.SKU,
                    sp.Quantity,
                    sp.SellingPrice,
                    sp.TotalItem
                )).ToList()
            ));

        return new PagedResponse<GetAllSalesQueryResponse>(pagedResponse);
    }
}