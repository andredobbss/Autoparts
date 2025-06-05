using MediatR;
namespace Autoparts.Api.Features.Users.DeleteCommand;

public sealed record DeleteUserCommand(string Name) :IRequest;