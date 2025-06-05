using MediatR;
namespace Autoparts.Api.Features.Sales.UpdateCommand;

public sealed record UpdateSaleCommand(string Name) :IRequest;