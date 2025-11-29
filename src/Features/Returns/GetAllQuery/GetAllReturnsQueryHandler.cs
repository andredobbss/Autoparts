using AutoMapper;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryHandler(IReturnRepository returnRepository, IMapper mapper) : IRequestHandler<GetAllReturnsQuery, PagedResponse<GetAllReturnsQueryResponse>>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllReturnsQueryResponse>> Handle(GetAllReturnsQuery request, CancellationToken cancellationToken)
    {
        var returns = await _returnRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var returnsReponse = _mapper.Map<IEnumerable<GetAllReturnsQueryResponse>>(returns.ToList());

        var returnsPaged = new StaticPagedList<GetAllReturnsQueryResponse>(returnsReponse, returns.GetMetaData());

        return new PagedResponse<GetAllReturnsQueryResponse>(returnsPaged);
    }
}