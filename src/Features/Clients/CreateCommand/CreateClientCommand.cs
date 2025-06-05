using MediatR;
namespace Autoparts.Api.Features.Clients.CreateCommand;

public sealed record CreateClientCommand(string Name) :IRequest;