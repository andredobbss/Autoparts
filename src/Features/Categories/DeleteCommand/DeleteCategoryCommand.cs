using MediatR;
namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed record DeleteCategoryCommand(string Name) :IRequest;