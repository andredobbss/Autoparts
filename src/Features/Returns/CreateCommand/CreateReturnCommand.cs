using Autoparts.Api.Features.Returns.Domain;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Justification, 
                                         string InvoiceNumber, 
                                         Guid UserId, 
                                         Guid ClientId, 
                                         IEnumerable<ProductsDto> Products) : IRequest<ValidationResult>;