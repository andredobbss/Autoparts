using MediatR;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQuery() : IRequest<GetAllReturnsQueryResponse>;
