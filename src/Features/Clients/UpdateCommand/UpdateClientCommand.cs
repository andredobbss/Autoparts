using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Clients.UpdateCommand;

public sealed record UpdateClientCommand(Guid ClientId,
                                         string ClientName,
                                         string? Email,
                                         ETaxIdType? TaxIdType,
                                         string? TaxId,
                                         Address Address) : IRequest<ValidationResult>;