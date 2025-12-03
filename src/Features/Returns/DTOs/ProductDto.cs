namespace Autoparts.Api.Features.Returns.DTOs;

public sealed record ProductDto(Guid ProductId,
                               int Quantity,
                               bool Loss);
