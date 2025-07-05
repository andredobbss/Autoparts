using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<GetAllManufacturersQuery, PagedResponse<Manufacturer>>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<PagedResponse<Manufacturer>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        var manufacture = await _manufacturerRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new PagedResponse<Manufacturer>(manufacture);
    }
}