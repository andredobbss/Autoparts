using Autoparts.Api.Shared.Products.DTOs;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryResponse(Guid ClientId,
                                                  string ClientName,
                                                  DateTime CreatedAt,
                                                  IEnumerable<ProductDto> Products);
