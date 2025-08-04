using Autoparts.Api.Features.Returns.Domain;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed record UpdateReturnCommand(Guid ReturnId,
                                         string Justification,
                                         string InvoiceNumber,
                                         Guid UserId,
                                         Guid ClientId,
                                         IEnumerable<ProductsDto> Products) : IRequest<ValidationResult>;