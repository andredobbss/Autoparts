using Autoparts.Api.Features.Users.Domain;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResponse>>
{
    public async Task<IEnumerable<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync();

        return users.Select(user => new GetAllUsersQueryResponse
        (
            user.Id,
            user.UserName!,
            user.Email!,
            user.TaxId!,
            user.Address
        )).ToList();
    }
}