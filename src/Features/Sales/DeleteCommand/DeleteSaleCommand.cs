using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Sales.DeleteCommand;

public sealed record DeleteSaleCommand(Guid SaleId) : IRequest<ValidationResult>;