using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Purchases.UpdateCommand;

public sealed record UpdatePurchaseCommand(Guid PurchaseId,
                                           string InvoiceNumber,
                                           EPaymentMethod PaymentMethod,
                                           Guid UserId,
                                           Guid SupplierId,
                                           IEnumerable<SharedProductsDto> Products) : IRequest<ValidationResult>;