using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<GetManufacturerByIdQuery, Manufacturer>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<Manufacturer> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);
    }
}