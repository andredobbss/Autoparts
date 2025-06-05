using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQuery() : IRequest<GetReturnByIdQueryResponse>;
