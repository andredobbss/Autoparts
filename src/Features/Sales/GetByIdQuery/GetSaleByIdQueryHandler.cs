using AutoMapper;
using Autoparts.Api.Features.Sales.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Sales.GetByIdQuery;

public sealed record GetSaleByIdQueryHandler(ISaleRepository SaleRepository, IMapper mapper) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResponse>
{
    private readonly ISaleRepository _saleRepository = SaleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetSaleByIdQueryResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);

        var response = _mapper.Map<GetSaleByIdQueryResponse>(sale);

        return response;
    }
}