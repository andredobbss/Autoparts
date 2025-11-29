using MediatR;
namespace Autoparts.Api.Features.Sales.DeleteCommand;

public sealed record DeleteSaleCommand(Guid SaleId) :IRequest<bool>;