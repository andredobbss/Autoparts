using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.DTOs;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetManufacturerByIdQuery, GetManufacturerByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetManufacturerByIdQueryResponse> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
        var manufacturer = await _context.Manufacturers!.FindAsync(request.ManufacturerId, cancellationToken);
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