using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Purchases.Domain;

public sealed class PurchaseProduct
{
    private PurchaseProduct() { }
    public PurchaseProduct(Guid? purchaseId, Guid? productId, int quantity, decimal acquisitionCost)
    {
        PurchaseId = purchaseId;
        ProductId = productId;
        Quantity = quantity;
        AcquisitionCost = acquisitionCost;
        TotalItem = TotalItemCalculate(quantity, acquisitionCost);
    }

    public PurchaseProduct(string name, string sku, int quantity, decimal acquisitionCost, decimal totalItem)
    {
        Name = name;
        SKU = sku;
        Quantity = quantity;
        AcquisitionCost = acquisitionCost;
        TotalItem = TotalItemCalculate(quantity, acquisitionCost);
    }

    public string? Name { get; private set; }
    public string? SKU { get; private set; }
    public Guid? PurchaseId { get; private set; }
    public Guid? ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal AcquisitionCost { get; private set; } = 0m;
    public decimal TotalItem { get; private set; }

    public Purchase Purchase { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private static decimal TotalItemCalculate(int quantity, decimal acquisitionCost)
    {
        return acquisitionCost * quantity;
    }
}
