namespace Autoparts.Api.Features.Returns.DTOs;

public sealed record ProductInput(Guid ProductId, int Quantity, bool Loss);
