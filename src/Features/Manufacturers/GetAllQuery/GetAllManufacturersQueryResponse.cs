namespace Autoparts.Api.Features.Manufacturers.GetAllQuery;

public sealed record GetAllManufacturersQueryResponse(Guid ManufacturerId, string Description, DateTime CreatedAt, DateTime UpdatedAt);
