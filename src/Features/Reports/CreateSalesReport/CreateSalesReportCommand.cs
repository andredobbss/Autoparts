using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Reports.CreateSalesReport;

public sealed record CreateSalesReportCommand() : IRequest<string>;

