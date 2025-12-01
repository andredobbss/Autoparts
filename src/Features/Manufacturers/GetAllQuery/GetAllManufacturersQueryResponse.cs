using Autoparts.Api.Shared.Products.Dto;

namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryResponse(Guid ManufacturerId,
                                                      string Description,
                                                      DateTime CreatedAt,
                                                      IEnumerable<ProductDto> Products);
