using Autoparts.Api.Features.Reports.Constants;
using Autoparts.Api.Features.Reports.DTOs;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FastReport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Reports.CreateSalesReport;

public sealed class CreateSalesReportCommandHandler(AutopartsDbContext context) : IRequestHandler<CreateSalesReportCommand, string>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<string> Handle(CreateSalesReportCommand request, CancellationToken cancellationToken)
    {
        var products = await _context.Database
                                       .SqlQueryRaw<ProductsList>(ConstantSql.SalesReportSql)
                                       .ToListAsync(cancellationToken);

        var reportPath = Path.Combine("Features", "Reports", "SalesReport.frx");
        if (File.Exists(reportPath))
            throw new ArgumentException(string.Format(Resource.REPORT_ALREADY_EXISTIS, reportPath));

        var reportFile = reportPath;
        var freport = new Report();
        freport.Dictionary.RegisterBusinessObject(products, "products", 10, true);
        freport.Report.Save(reportFile);
        return reportPath;
    }

}
