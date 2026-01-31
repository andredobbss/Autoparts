using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed record DeletePurchaseCommand(Guid PurchaseId) : IRequest<ValidationResult>;