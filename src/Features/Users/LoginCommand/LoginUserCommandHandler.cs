using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.DTOs;
using Autoparts.Api.Features.Users.Services;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.SimpleMediator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Autoparts.Api.Features.Users.LoginCommand;

public sealed class LoginUserCommandHandler(UserManager<User> _userManager, AutopartsDbContext _context, ITokenService _tokenRepository, IConfiguration _configuration) : IRequestHandler<LoginUserCommand, Token>
{
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

        // --------------------- Add Sales Ids as Claims -------------------------------------------
        var userQuery = _context.Sales
           .AsNoTracking()
           .Where(u => u.UserId == user.Id);

        //var userQuery = _context.Users
        //   .AsNoTracking()
        //   .Include(u => u.Sales)
        //   .Where(u => u.UserName == request.UserName);

        foreach (var users in userQuery)
        {
            //foreach (var sales in users.Sales)
            //{
            //    var saleId = sales.SaleId.ToString();
            //    authClaims.Add(new Claim("SaleId", saleId));
            //}

            var saleId = users.SaleId.ToString();
            authClaims.Add(new Claim("SaleId", saleId));
        }
        // --------------------------------------------------------------------------------------------

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
