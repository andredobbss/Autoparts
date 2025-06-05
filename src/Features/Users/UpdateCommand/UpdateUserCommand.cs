using MediatR;
namespace Autoparts.Api.Features.Users.UpdateCommand;

public sealed record UpdateUserCommand(string Name) :IRequest;