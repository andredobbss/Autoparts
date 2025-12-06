using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.DTOs;
using Autoparts.Api.Features.Users.Services;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Autoparts.Api.Features.Users.RefreshTokenCommand;

public sealed class RefreshTokenUserCommandHandler(UserManager<User> userManager, ITokenService tokenRepository, IConfiguration configuration) : IRequestHandler<RefreshTokenUserCommand, Token>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenRepository = tokenRepository;
    private readonly IConfiguration _configuration = configuration;
    public async Task<Token> Handle(RefreshTokenUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        string? accessToken = request.AccessToken ?? throw new ArgumentNullException(nameof(request.AccessToken));

        string? refreshToken = request.RefreshToken ?? throw new ArgumentNullException(nameof(request.RefreshToken));

        var principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken, _configuration);

        if (principal is null)
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        string? username = principal.Identity?.Name ?? 
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        var user = await _userManager.FindByNameAsync(username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException(Resource.INVALID_ACCESS_TOKEN);

        var newJwtToken = _tokenRepository.GenerateJwtToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenRepository.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new Token
        (
            AccessToken: new JwtSecurityTokenHandler().WriteToken(newJwtToken),
            RefreshToken : newRefreshToken,
            null
        );
    }
}
