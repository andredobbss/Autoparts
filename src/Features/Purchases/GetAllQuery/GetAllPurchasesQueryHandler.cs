using AutoMapper;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;
public sealed record GetAllPurchasesQueryHandler(IPurchaseRepository purchaseRepository, IMapper mapper) : IRequestHandler<GetAllPurchasesQuery, PagedResponse<GetAllPurchasesQueryResponse>>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllPurchasesQueryResponse>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _purchaseRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var purchasesResponse = _mapper.Map<IEnumerable<GetAllPurchasesQueryResponse>>(purchases.ToList());

        var purchasesPaged = new StaticPagedList<GetAllPurchasesQueryResponse>(purchasesResponse, purchases.GetMetaData());

        return new PagedResponse<GetAllPurchasesQueryResponse>(purchasesPaged);
    }
}