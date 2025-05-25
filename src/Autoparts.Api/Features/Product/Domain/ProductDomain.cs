namespace Autoparts.Api.Features.Product.Domain;

public class ProductDomain
{
    protected ProductDomain() { }
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public int CategoryId { get; private set; }
}
