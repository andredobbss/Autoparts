using Autoparts.Api.Features.Users.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<IEnumerable<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return users.Select(user => new GetAllUsersQueryResponse
        (
            user.Id,
            user.UserName,
            user.Email,
            user.TaxId,
            user.Address
        )).ToList();
    }
}