using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Sales.UpdateCommand;

public sealed record UpdateSaleCommand(Guid SaleId,
                                       string InvoiceNumber,
                                       EPaymentMethod PaymentMethod,
                                       Guid UserId,
                                       Guid ClientId,
                                       IEnumerable<LineItemDto> Products) : IRequest<ValidationResult>;