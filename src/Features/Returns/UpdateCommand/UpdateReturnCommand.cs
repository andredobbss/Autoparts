using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed record UpdateReturnCommand(Guid ReturnId,
                                         string Justification,
                                         string InvoiceNumber,
                                         EPaymentMethod PaymentMethod,
                                         bool Loss,
                                         Guid UserId,
                                         Guid ClientId,
                                         IEnumerable<LineItemDto> Products) : IRequest<ValidationResult>;