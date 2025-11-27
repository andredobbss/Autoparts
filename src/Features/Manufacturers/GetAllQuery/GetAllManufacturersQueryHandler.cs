using AutoMapper;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository, IMapper mapper) : IRequestHandler<GetAllManufacturersQuery, PagedResponse<GetAllManufacturersQueryResponse>>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedResponse<GetAllManufacturersQueryResponse>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        var manufactures = await _manufacturerRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var manufacturesResponse = _mapper.Map<IEnumerable<GetAllManufacturersQueryResponse>>(manufactures.ToList());

        var pagedResponse = new StaticPagedList<GetAllManufacturersQueryResponse>(manufacturesResponse, manufactures.GetMetaData());

        return new PagedResponse<GetAllManufacturersQueryResponse>(pagedResponse);
    }
}