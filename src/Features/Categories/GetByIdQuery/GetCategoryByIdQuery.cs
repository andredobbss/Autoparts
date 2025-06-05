using MediatR;

namespace Autoparts.Api.Features.Categories.GetByIdQuery;

public sealed record GetCategoryByIdQuery() : IRequest<GetCategoryByIdQueryResponse>;
