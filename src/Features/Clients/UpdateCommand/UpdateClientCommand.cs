using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.UpdateCommand;

public sealed record UpdateClientCommand(Guid ClientId, string ClientName, Address Address) : IRequest<ValidationResult>;