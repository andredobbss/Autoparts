using Autoparts.Api.Shared.Resources;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Autoparts.Api.Features.Users.Services;

internal sealed class TokenService : ITokenService
{
    public JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
            throw new InvalidOperationException(Resource.INVALID_SECRET_KEY);

        var privateKey = Encoding.UTF8.GetBytes(key);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenvalidity = configuration.GetSection("JWT").GetValue<int>("TokenValidityInMinutes");
        var validAudience = configuration.GetSection("JWT").GetValue<string>("ValidAudience");
        var validIssuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(tokenvalidity),
            Audience = validAudience,
            Issuer = validIssuer,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, IConfiguration configuration)
    {
        var secretKey = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
            throw new ArgumentException(Resource.INVALID_SECRET_KEY);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException(Resource.INVALID_SECRET_KEY);
        }

        return principal;
    }
}
