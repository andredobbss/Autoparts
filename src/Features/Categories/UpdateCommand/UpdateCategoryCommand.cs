using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Categories.UpdateCommand;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Description) :IRequest<ValidationResult>;