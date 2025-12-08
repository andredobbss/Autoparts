using MediatR;

namespace Autoparts.Api.Features.Reports.CreateSalesReport;

public sealed record CreateSalesReportCommand() : IRequest<string>;

