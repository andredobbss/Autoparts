using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Returns.DeleteCommand;

public sealed record DeleteReturnCommand(Guid ReturnId) : IRequest<ValidationResult>;