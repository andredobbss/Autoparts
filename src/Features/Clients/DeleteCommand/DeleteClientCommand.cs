using MediatR;
namespace Autoparts.Api.Features.Clients.DeleteCommand;

public sealed record DeleteClientCommand(string Name) :IRequest;