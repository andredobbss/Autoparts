using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed record DeletePurchaseCommand(Guid PurchaseId) : IRequest<ValidationResult>;