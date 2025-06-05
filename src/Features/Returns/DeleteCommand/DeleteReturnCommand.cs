using MediatR;
namespace Autoparts.Api.Features.Returns.DeleteCommand;

public sealed record DeleteReturnCommand(string Name) :IRequest;