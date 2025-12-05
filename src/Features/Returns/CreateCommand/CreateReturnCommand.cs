using Autoparts.Api.Features.Returns.DTOs;
using Autoparts.Api.Shared.Enums;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Justification,
                                         string InvoiceNumber,
                                         decimal TotalSale,
                                         EPaymentMethod PaymentMethod,
                                         Guid UserId,
                                         Guid ClientId,
                                         IEnumerable<ProductInput> Products) : IRequest<ValidationResult>;