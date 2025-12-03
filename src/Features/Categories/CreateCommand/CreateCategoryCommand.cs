using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.CreateCommand;

public sealed record CreateCategoryCommand(string Description) : IRequest<ValidationResult>;