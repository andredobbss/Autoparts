using Autoparts.Api.Features.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResponse>>
{
    private readonly UserManager<User> _userManager = userManager;
    public async Task<IEnumerable<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync();

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