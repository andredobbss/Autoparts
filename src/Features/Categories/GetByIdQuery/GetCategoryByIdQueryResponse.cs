namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryResponse(Guid CategoryId, string Description, DateTime CreatedAt, DateTime UpdatedAt);
