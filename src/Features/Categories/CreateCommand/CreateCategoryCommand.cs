using Autoparts.Api.Features.Categories.Domain;
using MediatR;
namespace Autoparts.Api.Features.Categories.CreateCommand;

public sealed record CreateCategoryCommand(string description) : IRequest<Category>;