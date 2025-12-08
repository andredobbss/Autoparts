using Autoparts.Api.Infraestructure.Persistence;
using FastReport;
using FastReport.Export.PdfSimple;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Reports.GetSalesReport;

public sealed class GetSalesReportHandler(AutopartsDbContext context) : IRequestHandler<GetSalesReportQuery, byte[]>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<byte[]> Handle(GetSalesReportQuery request, CancellationToken cancellationToken)
    {
        // Carrega vendas do período
        var sales = await _context.Sales!
            .Include(x => x.SaleProducts)
            .ThenInclude(x => x.Product)
            .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt <= request.EndDate)
            .ToListAsync(cancellationToken);

        // Carrega estoque
        var products = await _context.Products!.ToListAsync(cancellationToken);

        // monta dataset
        var result = new SalesReportResult();

        foreach (var product in products)
        {
            var itemsSold = sales
                .SelectMany(s => s.SaleProducts)
                .Where(i => i.ProductId == product.ProductId)
                .ToList();

            var qty = itemsSold.Sum(i => i.Quantity);

            var lastSaleDate = itemsSold
                .Select(i => i.Sale.CreatedAt)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();

            var item = new SalesReportItem
            {
                ProductName = product.Name,
                SellingPrice = product.SellingPrice,
                AcquisitionCost = product.AcquisitionCost,
                QuantitySold = qty,
                CurrentStock = product.Stock,
                IsStopped = (DateTime.UtcNow - lastSaleDate).TotalDays > 90
            };

            result.Items.Add(item);
        }

        // carrega o relatório FastReport
        var reportPath = Path.Combine("Features", "Reports", "GetSalesReport", "SalesReport.frx");

        Report report = new();
        report.Load(reportPath);

        // passa dataset para o relatório
        report.RegisterData(result.Items, "Items");
        report.RegisterData(new[] { result }, "Header");

        using var pdfStream = new MemoryStream();

        report.Prepare();
        report.Export(new PDFSimpleExport(), pdfStream);

        return pdfStream.ToArray();
    }
}
