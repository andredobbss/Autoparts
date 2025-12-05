using MediatR;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdQueryResponse>;
