namespace Autoparts.Api.Features.Manufacturers.GetByIdQuery;

public sealed record GetManufacturerByIdQueryResponse(Guid ManufacturerId, string Description, DateTime CreatedAt, DateTime UpdatedAt);
