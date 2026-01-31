using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed record DeleteCategoryCommand(Guid CategoryId) : IRequest<ValidationResult>;