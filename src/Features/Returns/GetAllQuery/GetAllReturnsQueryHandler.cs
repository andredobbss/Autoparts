using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryHandler(IReturnRepository returnRepository) : IRequestHandler<GetAllReturnsQuery, PagedResponse<GetAllReturnsQueryResponse>>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<PagedResponse<GetAllReturnsQueryResponse>> Handle(GetAllReturnsQuery request, CancellationToken cancellationToken)
    {
        var returns = await _returnRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = returns
            .Select(r => new GetAllReturnsQueryResponse
        (
            r.ReturnId,
            r.Justification,
            r.InvoiceNumber,
            r.CreatedAt,
            r.User.UserName,
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