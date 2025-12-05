using Autoparts.Api.Shared.Products.DTOs;

namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryResponse(Guid ManufacturerId,
                                                      string Description,
                                                      DateTime CreatedAt,
                                                      IEnumerable<ProductDto> Products);
