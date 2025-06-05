using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryHandler():IRequestHandler<GetReturnByIdQuery,GetReturnByIdQueryResponse>
{
    public async Task<GetReturnByIdQueryResponse> Handle(GetReturnByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}