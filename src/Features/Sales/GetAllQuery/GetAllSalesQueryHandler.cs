using AutoMapper;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Sales.GetAllQuery;

public sealed record GetAllSalesQueryHandler(ISaleRepository SaleRepository, IMapper mapper) : IRequestHandler<GetAllSalesQuery, PagedResponse<GetAllSalesQueryResponse>>
{
    private readonly ISaleRepository _saleRepository = SaleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedResponse<GetAllSalesQueryResponse>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var salesResponse = _mapper.Map<List<GetAllSalesQueryResponse>>(sales.ToList());

        var pagedResponse = new StaticPagedList<GetAllSalesQueryResponse>(salesResponse, sales.GetMetaData());

        return new PagedResponse<GetAllSalesQueryResponse>(pagedResponse);
    }
}