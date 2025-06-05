using MediatR;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQuery() : IRequest<GetAllCategoriesQueryResponse>;
