namespace Autoparts.Api.Features.Returns.Domain
{
    public sealed record ProductsDto(Guid ProductId, int Quantity, bool Loss);

}
