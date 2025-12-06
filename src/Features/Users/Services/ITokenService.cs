using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Autoparts.Api.Features.Users.Services;

public interface ITokenService
{
    JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims, IConfiguration configuration);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, IConfiguration configuration);
}
