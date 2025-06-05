using MediatR;
namespace Autoparts.Api.Features.Clients.UpdateCommand;

public sealed record UpdateClientCommand(string Name) :IRequest;