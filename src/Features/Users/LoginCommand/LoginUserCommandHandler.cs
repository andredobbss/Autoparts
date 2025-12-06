using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.DTOs;
using Autoparts.Api.Features.Users.Services;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Autoparts.Api.Features.Users.LoginCommand;

public sealed class LoginUserCommandHandler(UserManager<User> userManager, ITokenService tokenRepository, IConfiguration configuration) : IRequestHandler<LoginUserCommand, Token>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenRepository = tokenRepository;
    private readonly IConfiguration _configuration = configuration;
    public async Task<Token> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException(Resource.INVALID_LOGIN);

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in userRoles)
            authClaims.Add(new(ClaimTypes.Role, role));

        var token = _tokenRepository.GenerateJwtToken(authClaims, _configuration);

        var refreshToken = _tokenRepository.GenerateRefreshToken();

        _ = int.TryParse(
            _configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

        await _userManager.UpdateAsync(user);

        return new Token(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken: refreshToken,
            RefreshTokenExpiryTime: token.ValidTo
        );
    }
}
