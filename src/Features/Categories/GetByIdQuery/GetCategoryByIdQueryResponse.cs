namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQueryResponse(Guid ClientId,
                                                  string ClientName,
                                                  DateTime CreatedAt);
