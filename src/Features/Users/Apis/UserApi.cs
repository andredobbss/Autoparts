using Autoparts.Api.Features.Users.CreateCommand;
using Autoparts.Api.Features.Users.DeactiveCommand;
using Autoparts.Api.Features.Users.DeleteCommand;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.GetAllQuery;
using Autoparts.Api.Features.Users.GetByIdQuery;
using Autoparts.Api.Features.Users.LoginCommand;
using Autoparts.Api.Features.Users.LogoutCommand;
using Autoparts.Api.Features.Users.RefreshTokenCommand;
using Autoparts.Api.Features.Users.RevokeCommand;
using Autoparts.Api.Features.Users.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Autoparts.Api.Features.Users.Apis;

public static class UserApi
{
    public static void MapUserApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
        .WithTags("Users");

        //group.MapIdentityApi<User>();

        group.MapGet("/roles", (ClaimsPrincipal user) =>
        {
            //if (user.Identity is null || user.Identity.IsAuthenticated is false)
            //    return Results.Unauthorized();

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
        });
        //.RequireAuthorization();

        group.MapGet("/", async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetAllUsersQuery());
            return Results.Ok(result);
        });

        group.MapGet("/{id:guid}", async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return Results.Ok(result);
        });

        group.MapPost("/login", async (LoginUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("/logout", async (LogoutUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("/refresh-token", async (RefreshTokenUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("/revoke-refresh-token", async (RevokeRefreshTokenUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("/", async ([FromBody] CreateUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Created($"/api/users/{result}", result) : Results.BadRequest(result.Errors);
        });

        group.MapPut("/", async ([FromBody] UpdateUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Errors);
        });

        group.MapDelete("/{id:guid}", async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            return result.Succeeded ? Results.NoContent() : Results.NotFound(result.Errors);
        });

        group.MapPatch("/userDeactive/{id:guid}", async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new DeactiveUserCommand(id));
            return result.Succeeded ? Results.NoContent() : Results.NotFound(result.Errors);
        });
    }
}
