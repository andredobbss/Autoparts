using Autoparts.Api.Features.Categories.Domain;
using MediatR;
namespace Autoparts.Api.Features.Categories.UpdateCommand;

public sealed record UpdateCategoryCommand(Guid id, string description) :IRequest<Category>;