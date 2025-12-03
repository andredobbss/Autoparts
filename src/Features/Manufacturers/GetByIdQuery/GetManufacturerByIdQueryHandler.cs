using Autoparts.Api.Features.Manufacturers.DTOs;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<GetManufacturerByIdQuery, GetManufacturerByIdQueryResponse>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<GetManufacturerByIdQueryResponse> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            throw new KeyNotFoundException(string.Format(Resource.MANUFACTORER_NOT_FOUND, request.ManufacturerId));

        return new GetManufacturerByIdQueryResponse
            (
            manufacturer.ManufacturerId,
            manufacturer.Description,
            manufacturer.CreatedAt,
            manufacturer.Products.Select(pp => new ProductDto
                (
                    pp.ProductId,
                    pp.Name,
                    pp.TechnicalDescription,
                    pp.SKU,
                    pp.Compatibility,
                    pp.AcquisitionCost,
                    pp.SellingPrice
                )).ToList()
            );
    }
}