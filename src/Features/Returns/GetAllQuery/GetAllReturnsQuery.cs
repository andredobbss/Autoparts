using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Return>>;
