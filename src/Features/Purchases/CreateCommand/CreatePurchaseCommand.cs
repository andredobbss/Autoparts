using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.DTOs;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.CreateCommand;

public sealed record CreatePurchaseCommand(string InvoiceNumber,
                                           EPaymentMethod PaymentMethod,
                                           Guid UserId,
                                           Guid SupplierId,
                                           IEnumerable<LineItemDto> Products) : IRequest<ValidationResult>;



