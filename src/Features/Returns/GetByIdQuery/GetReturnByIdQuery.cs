using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQuery(Guid ReturnId) : IRequest<GetReturnByIdQueryResponse>;
