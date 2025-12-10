using Autoparts.Api.Features.Reports.CreateSalesReport;
using Autoparts.Api.Features.Reports.GetSalesReport;
using MediatR;

namespace Autoparts.Api.Features.Reports.Apis;

public static class SaleReportApi
{
    public static void MapSaleReportApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sale-report")
            .WithTags("Sale Reports")
            .RequireAuthorization("ManagerOnly");
        group.MapGet("/", GetSaleReport);
        group.MapPost("/create-sales-report", CreateSalesReport);
    }

    private static async Task<IResult> CreateSalesReport(CreateSalesReportCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetSaleReport(DateTime startDate, DateTime endDate, ISender mediator)
    {
        var pdfBytes = await mediator.Send(new GetSalesReportQuery(startDate, endDate));

        return Results.File(
            fileContents: pdfBytes,
            contentType: "application/pdf",
            fileDownloadName: $"sales-report-{startDate:yyyyMMdd}-{endDate:yyyyMMdd}.pdf"
        );
    }
}
