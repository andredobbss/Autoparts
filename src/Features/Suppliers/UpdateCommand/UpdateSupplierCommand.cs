using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.UpdateCommand;

public sealed record UpdateSupplierCommand(Guid SupplierId,
                                           string CompanyName,
                                           string? Email,
                                           ETaxIdType? TaxIdType,
                                           string? TaxId,
                                           Address Address) : IRequest<ValidationResult>;