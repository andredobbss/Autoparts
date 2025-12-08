namespace Autoparts.Api.Features.Reports.GetSalesReport;

public class SalesReportResult
{
    public List<SalesReportItem> Items { get; set; } = new();
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}

public sealed class SalesReportItem
{
    public string ProductName { get; set; } = default!;
    public decimal SellingPrice { get; set; }
    public decimal AcquisitionCost { get; set; }
    public int QuantitySold { get; set; }
    public decimal Margin => SellingPrice - AcquisitionCost;
    public int CurrentStock { get; set; }
    public bool IsStopped { get; set; } // sem vendas 90+ dias
}

