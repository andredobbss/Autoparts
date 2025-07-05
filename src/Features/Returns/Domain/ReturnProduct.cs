using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Returns.Domain;

public class ReturnProduct
{
    private ReturnProduct() { }
    public ReturnProduct(Guid returnId, Guid productId, int quantity, decimal sellingPrice)
    {
        ReturnId = returnId;
        ProductId = productId;
        Quantity = quantity;
        SellingPrice = sellingPrice;
        TotalItem = TotalItemCalculate(quantity, sellingPrice);
    }

    public Guid ReturnId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal SellingPrice { get; private set; } = 0m;
    public decimal TotalItem { get; private set; } = 0m;

    public Return Return { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private static decimal TotalItemCalculate(int quantity, decimal sellingPrice)
    {
        return sellingPrice * quantity;
    }
}
