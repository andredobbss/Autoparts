using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQueryHandler(UserManager<User> _userManager) :IRequestHandler<GetUserByIdQuery,GetUserByIdQueryResponse>
{
    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
            throw new KeyNotFoundException(string.Format(Resource.USER_NOT_FOUND, request.Id));

        return new GetUserByIdQueryResponse
        (
            user.Id,
            user.UserName!,
            user.Email!,
            user.TaxId!,
            user.Address
        );
    }
}