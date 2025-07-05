using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.CreateCommand;

public sealed record CreateSupplierCommand(string CompanyName, Address Address) :IRequest<ValidationResult>;