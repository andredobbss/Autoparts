using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Autoparts.Api.Features.Users.Apis;

public static class UserApi
{
    public static void MapUserApi(this IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup("/api/users")
        //      .WithTags("Users")
        //      .MapIdentityApi<User>();


        var group = app.MapGroup("/api/users")
        .WithTags("Users");

        group.MapIdentityApi<User>();

        group.MapPost("/logout", async (SignInManager<User> signin) =>
        {
            await signin.SignOutAsync();

            return Results.Ok();
        })
        .RequireAuthorization();


        group.MapGet("/roles", (ClaimsPrincipal user) =>
        {
            if (user.Identity is null || user.Identity.IsAuthenticated is false)
                return Results.Unauthorized();

            var identity = user.Identity as ClaimsIdentity;

            var roles = identity
                .FindAll(identity.RoleClaimType)
                .Select(c => new
                {
                    c.Issuer,
                    c.OriginalIssuer,
                    c.Value,
                    c.Type,
                    c.ValueType
                })
                .ToList();


            return TypedResults.Json(roles);
        })
            .RequireAuthorization();

        //group.MapGet("/", async (AutopartsDbContext db) =>
        //{
        //    var users = await db.Users
        //        .Select(u => new
        //        {
        //            u.Id,
        //            u.UserName,
        //            u.Email,
        //            Address = new
        //            {
        //                u.Address.Street,
        //                u.Address.Number,
        //                u.Address.City,
        //                u.Address.State,
        //                u.Address.ZipCode
        //            }
        //        })
        //        .ToListAsync();

        //    return Results.Ok(users);
        //});

        //group.MapGet("/{id:guid}", async (Guid id, AutopartsDbContext db) =>
        //{
        //    var user = await db.Users
        //        .Where(u => u.Id == id)
        //        .Select(u => new
        //        {
        //            u.Id,
        //            u.UserName,
        //            u.Email,
        //            Address = new
        //            {
        //                u.Address.Street,
        //                u.Address.Number,
        //                u.Address.City,
        //                u.Address.State,
        //                u.Address.ZipCode
        //            }
        //        })
        //        .FirstOrDefaultAsync();
        //    return user is not null ? Results.Ok(user) : Results.NotFound();
        //});

        //group.MapPost("/", async (User user, AutopartsDbContext db) =>
        //{
        //    if (user is null)
        //    {
        //        return Results.BadRequest("User cannot be null.");
        //    }
        //    db.Users.Add(user);
        //    await db.SaveChangesAsync();
        //    return Results.Created($"/api/users/{user.Id}", user);
        //});
    }
}
