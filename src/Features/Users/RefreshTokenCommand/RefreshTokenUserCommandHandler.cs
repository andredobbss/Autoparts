using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.DTOs;
using Autoparts.Api.Features.Users.Services;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Autoparts.Api.Features.Users.RefreshTokenCommand;

public sealed class RefreshTokenUserCommandHandler(UserManager<User> userManager, ITokenService tokenRepository, IConfiguration configuration) : IRequestHandler<RefreshTokenUserCommand, Token>
{
    public async Task<Token> Handle(RefreshTokenUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        string? accessToken = request.AccessToken ?? throw new ArgumentNullException(nameof(request.AccessToken));

        string? refreshToken = request.RefreshToken ?? throw new ArgumentNullException(nameof(request.RefreshToken));

        var principal = tokenRepository.GetPrincipalFromExpiredToken(accessToken, configuration);

        if (principal is null)
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        string? username = principal.Identity?.Name ??
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        var user = await userManager.FindByNameAsync(username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        var newJwtToken = tokenRepository.GenerateJwtToken(principal.Claims.ToList(), configuration);

        var newRefreshToken = tokenRepository.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await userManager.UpdateAsync(user);

        return new Token
        (
            AccessToken: new JwtSecurityTokenHandler().WriteToken(newJwtToken),
            RefreshToken: newRefreshToken,
            null
        );
    }
}
