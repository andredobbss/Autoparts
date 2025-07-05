using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.UpdateCommand;

public sealed record UpdateClientCommand(Guid ClientId, string Name, Address Address) : IRequest<ValidationResult>;