using MediatR;
namespace Autoparts.Api.Features.Sales.CreateCommand;

public sealed record CreateSaleCommand(string Name) :IRequest;