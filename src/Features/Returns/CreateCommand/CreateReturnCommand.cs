using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;

public sealed record CreateReturnCommand(string Name) :IRequest;