using MediatR;

namespace Autoparts.Api.Features.Users.CreateCommand;

public sealed record CreateUserCommand(string Name) : IRequest;