using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.CreateCommand;

public sealed record CreateSupplierCommand(string CompanyName,
                                           string? Email,
                                           ETaxIdType? TaxIdType,
                                           string? TaxId,
                                           Address Address) : IRequest<ValidationResult>;