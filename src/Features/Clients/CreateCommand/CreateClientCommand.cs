using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.CreateCommand;

public sealed record CreateClientCommand(string Name, Address Address) : IRequest<ValidationResult>;