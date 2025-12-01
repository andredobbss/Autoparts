namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryResponse(Guid CategoryId,
                                                   string Description,
                                                   DateTime CreatedAt);
