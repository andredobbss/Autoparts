using Autoparts.Api.Features.Manufacturers.DTOs;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryResponse(Guid ManufacturerId,
                                                      string Description,
                                                      DateTime CreatedAt,
                                                      IEnumerable<ProductDto> Products);
