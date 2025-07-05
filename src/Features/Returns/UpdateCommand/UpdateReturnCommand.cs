using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed record UpdateReturnCommand(Guid ReturnId, string Justification, string InvoiceNumber, uint Quantity, bool Loss, Guid UserId, Guid ClientId, Guid ProductId) : IRequest<ValidationResult>;