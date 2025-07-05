using Autoparts.Api.Features.Categories.Domain;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQuery(Guid CategoryId) : IRequest<Category>;
