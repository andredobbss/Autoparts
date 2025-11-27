using AutoMapper;
using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Products.GetAllQuery;

public sealed record GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, PagedResponse<GetAllProductsQueryResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllProductsQueryResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var produtsResponse = _mapper.Map<IEnumerable<GetAllProductsQueryResponse>>(products.ToList());

        var pagedResponse = new StaticPagedList<GetAllProductsQueryResponse>(produtsResponse, products.GetMetaData());

        return new PagedResponse<GetAllProductsQueryResponse>(pagedResponse);
    }
}