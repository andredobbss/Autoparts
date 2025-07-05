using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.UpdateCommand;

public sealed record UpdateSupplierCommand(Guid SupplierId, string CompanyName, Address Address) :IRequest<ValidationResult>;