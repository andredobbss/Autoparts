using Autoparts.Api.Features.Returns.DTOs;
using Autoparts.Api.Shared.Enums;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed record UpdateReturnCommand(Guid ReturnId,
                                         string Justification,
                                         string InvoiceNumber,
                                         EPaymentMethod PaymentMethod,
                                         Guid UserId,
                                         Guid ClientId,
                                         IEnumerable<ProductDto> Products) : IRequest<ValidationResult>;