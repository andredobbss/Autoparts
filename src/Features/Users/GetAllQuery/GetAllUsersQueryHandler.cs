using MediatR;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQueryHandler():IRequestHandler<GetAllUsersQuery,GetAllUsersQueryResponse>
{
    public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}