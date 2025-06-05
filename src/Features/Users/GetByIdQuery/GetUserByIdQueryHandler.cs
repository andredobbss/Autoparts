using MediatR;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQueryHandler():IRequestHandler<GetUserByIdQuery,GetUserByIdQueryResponse>
{
    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}