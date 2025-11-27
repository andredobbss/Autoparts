using AutoMapper;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryHandler(IManufacturerRepository manufacturerRepository, IMapper mapper) : IRequestHandler<GetManufacturerByIdQuery, GetManufacturerByIdQueryResponse>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetManufacturerByIdQueryResponse> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);

        return _mapper.Map<GetManufacturerByIdQueryResponse>(manufacturer);
    }
}