using MediatR;
namespace Autoparts.Api.Features.Clients.DeleteCommand;

public sealed record DeleteClientCommand(Guid ClientId) : IRequest<bool>;