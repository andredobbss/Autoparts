using Autoparts.Api.Shared.Products.Dto;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Justification, 
                                         string InvoiceNumber, 
                                         bool Loss, 
                                         Guid UserId, 
                                         Guid ClientId, 
                                         Guid ProductId,
                                         IEnumerable<SharedProductsDto> Products) : IRequest<ValidationResult>;