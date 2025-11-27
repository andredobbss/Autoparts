using AutoMapper;
using Autoparts.Api.Features.Products.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Products.GetByIdQuery;

public sealed record GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        return _mapper.Map<GetProductByIdQueryResponse>(product);
    }
}