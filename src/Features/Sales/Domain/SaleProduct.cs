using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Sales.Domain;

public class SaleProduct
{
    private SaleProduct() { }
    public SaleProduct(Guid saleId, Guid productId, int quantity, decimal sellingPrice)
    {
        SaleId = saleId;
        ProductId = productId;
        Quantity = quantity;
        SellingPrice = sellingPrice;
        TotalItem = TotalItemCalculate(quantity, sellingPrice);
    }

    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal SellingPrice { get; private set; } = 0m;
    public decimal TotalItem { get; private set; } = 0m;

    public Sale Sale { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private static decimal TotalItemCalculate(int quantity, decimal sellingPrice)
    {
        return sellingPrice * quantity;
    }
}
