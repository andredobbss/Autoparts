using Autoparts.Api.Features.Users.Domain;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed class GetAllUserRolesQueryHandler(UserManager<User> userManager) : IRequestHandler<GetAllUserRolesQuery, IEnumerable<GetAllUserRolesQueryResponse>>
{
    public async Task<IEnumerable<GetAllUserRolesQueryResponse>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);

        var result = new List<GetAllUserRolesQueryResponse>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);

            result.Add(new GetAllUserRolesQueryResponse(
                user.UserName!,
                roles
            ));
        }

        return result;
    }
}
