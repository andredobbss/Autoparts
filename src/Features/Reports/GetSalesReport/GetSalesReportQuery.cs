using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Reports.GetSalesReport;

public sealed record GetSalesReportQuery(DateTime StartDate, DateTime EndDate) : IRequest<byte[]>;

