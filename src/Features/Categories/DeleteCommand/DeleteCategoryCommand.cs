using MediatR;
namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed record DeleteCategoryCommand(Guid CategoryId) : IRequest<bool>;