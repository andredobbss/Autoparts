using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Suppliers.DeleteCommand;

public sealed record DeleteSupplierCommand(Guid SupplierId) : IRequest<ValidationResult>;