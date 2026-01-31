using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Clients.DeleteCommand;

public sealed record DeleteClientCommand(Guid ClientId) : IRequest<ValidationResult>;