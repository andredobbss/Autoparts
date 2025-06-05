using MediatR;
namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed record DeletePurchaseCommand(string Name) :IRequest;