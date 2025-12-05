using Autoparts.Api.Shared.Products.DTOs;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryResponse(Guid CategoryId,
                                                   string Description,
                                                   DateTime CreatedAt,
                                                   IEnumerable<ProductDto> Products);
