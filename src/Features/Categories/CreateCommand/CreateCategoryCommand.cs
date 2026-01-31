using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Categories.CreateCommand;

public sealed record CreateCategoryCommand(string Description) : IRequest<ValidationResult>;