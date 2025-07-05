using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Justification, string InvoiceNumber, uint Quantity, bool Loss, Guid UserId, Guid ClientId, Guid ProductId) : IRequest<ValidationResult>;