using Autoparts.Api.Features.Users.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQueryHandler(IUserRepository userRepository) :IRequestHandler<GetUserByIdQuery,GetUserByIdQueryResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException(string.Format(Resource.USER_NOT_FOUND, request.Id));

        return new GetUserByIdQueryResponse
        (
            user.Id,
            user.UserName,
            user.Email,
            user.TaxId,
            user.Address
        );
    }
}