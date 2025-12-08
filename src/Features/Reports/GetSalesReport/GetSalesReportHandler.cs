using Autoparts.Api.Features.Reports.Constants;
using Autoparts.Api.Features.Reports.DTOs;
using Autoparts.Api.Infraestructure.Persistence;
using FastReport;
using FastReport.Export.PdfSimple;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Reports.GetSalesReport;

public sealed class GetSalesReportHandler(AutopartsDbContext context) : IRequestHandler<GetSalesReportQuery, byte[]>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<byte[]> Handle(GetSalesReportQuery request, CancellationToken cancellationToken)
    {
        var startParam = new SqlParameter("@startDate", request.StartDate);
        var endParam = new SqlParameter("@endDate", request.EndDate);

        var products = await _context.Database
                                      .SqlQueryRaw<ProductsList>(ConstantSql.SalesReportSqlWithParameters, startParam, endParam)
                                      .ToListAsync(cancellationToken);

        var reportPath = Path.Combine("Features", "Reports", "SalesReport.frx");
        var reportFile = reportPath;
        var freport = new Report();
        freport.Load(reportFile);
        freport.Dictionary.RegisterBusinessObject(products, "products", 10, true);
        freport.Prepare();
        var pdfExport = new PDFSimpleExport();
        using var pdfStream = new MemoryStream();

        pdfExport.Export(freport, pdfStream);

        return pdfStream.ToArray();
    }
}
