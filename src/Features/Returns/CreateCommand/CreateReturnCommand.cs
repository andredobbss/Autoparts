using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Justification,
                                         string InvoiceNumber,
                                         decimal TotalSale,
                                         EPaymentMethod PaymentMethod,
                                         bool Loss,
                                         Guid UserId,
                                         Guid ClientId,
                                         IEnumerable<SharedProductsDto> Products) : IRequest<ValidationResult>;