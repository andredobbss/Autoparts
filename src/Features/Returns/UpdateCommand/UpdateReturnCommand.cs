using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed record UpdateReturnCommand(string Name) :IRequest;