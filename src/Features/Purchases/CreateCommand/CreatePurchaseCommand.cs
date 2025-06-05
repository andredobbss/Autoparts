using MediatR;
namespace Autoparts.Api.Features.Purchases.CreateCommand;

public sealed record CreatePurchaseCommand(string Name) :IRequest;