using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using Autoparts.Api.Shared.Products.Dto;
using MediatR;
using Z.PagedList;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<GetAllManufacturersQuery, PagedResponse<GetAllManufacturersQueryResponse>>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<PagedResponse<GetAllManufacturersQueryResponse>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        var manufactures = await _manufacturerRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pagedResponse = manufactures
            .Select(m => new GetAllManufacturersQueryResponse
            (
            m.ManufacturerId,
            m.Description,
            m.CreatedAt,
            m.Products.Select(pp => new ProductDto
                (
                    pp.ProductId,
                    pp.Name,
                    pp.TechnicalDescription,
                    pp.SKU,
                    pp.Compatibility,
                    pp.AcquisitionCost,
                    pp.SellingPrice
                )).ToList()
                ));

        return new PagedResponse<GetAllManufacturersQueryResponse>(pagedResponse);
    }
}