using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Returns.Domain;

public class ReturnProduct
{
    private ReturnProduct() { }
    public ReturnProduct(Guid? returnId, Guid? productId, int quantity, decimal sellingPrice, bool loss)
    {
        ReturnId = returnId;
        ProductId = productId;
        Quantity = quantity;
        SellingPrice = sellingPrice;
        Loss = loss;
        TotalItem = TotalItemCalculate(Quantity, SellingPrice);
    }

    public ReturnProduct(string name, string sku, int quantity, decimal sellingPrice, decimal totalItem, bool loss)
    {
        Name = name;
        SKU = sku;
        Quantity = quantity;
        SellingPrice = sellingPrice;
        Loss = loss;
        TotalItem = TotalItemCalculate(Quantity, SellingPrice);
    }

    public string? Name { get; private set; }
    public string? SKU { get; private set; }
    public Guid? ReturnId { get; private set; }
    public Guid? ProductId { get; private set; }
    public int Quantity { get; private set; }
    public bool Loss { get; private set; } = false;
    public decimal SellingPrice { get; private set; } = 0m;
    public decimal TotalItem { get; private set; }

    public Return Return { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private static decimal TotalItemCalculate(int quantity, decimal sellingPrice)
    {
        return sellingPrice * quantity;
    }
}
