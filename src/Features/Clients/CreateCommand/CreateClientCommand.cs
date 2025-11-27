using Autoparts.Api.Shared.ValueObejct;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.CreateCommand;

public sealed record CreateClientCommand(string ClientName, Address Address) : IRequest<ValidationResult>;