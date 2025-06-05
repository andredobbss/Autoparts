using MediatR;
namespace Autoparts.Api.Features.Purchases.UpdateCommand;

public sealed record UpdatePurchaseCommand(string Name) :IRequest;