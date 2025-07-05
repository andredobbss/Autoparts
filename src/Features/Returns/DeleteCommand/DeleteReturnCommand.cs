using MediatR;
namespace Autoparts.Api.Features.Returns.DeleteCommand;

public sealed record DeleteReturnCommand(Guid ReturnId) : IRequest<bool>;