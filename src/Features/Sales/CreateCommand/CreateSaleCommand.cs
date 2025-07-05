using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Sales.CreateCommand;

public sealed record CreateSaleCommand() :IRequest<ValidationResult>;