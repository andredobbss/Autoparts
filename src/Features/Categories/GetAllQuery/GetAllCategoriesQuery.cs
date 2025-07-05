using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Category>>;
