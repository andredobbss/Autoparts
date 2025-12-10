using Autoparts.Api.Features.Users.CreateCommand;
using Autoparts.Api.Features.Users.DeactiveCommand;
using Autoparts.Api.Features.Users.DeleteCommand;
using Autoparts.Api.Features.Users.GetAllQuery;
using Autoparts.Api.Features.Users.GetByIdQuery;
using Autoparts.Api.Features.Users.LoginCommand;
using Autoparts.Api.Features.Users.LogoutCommand;
using Autoparts.Api.Features.Users.RefreshTokenCommand;
using Autoparts.Api.Features.Users.RevokeCommand;
using Autoparts.Api.Features.Users.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Users.Apis;

public static class UserApi
{
    public static void MapUserApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
        .WithTags("Users");

        //group.MapIdentityApi<User>();

        group.MapGet("/get-all-users", [Authorize(Policy = "AdminOnly")] async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetAllUsersQuery());
            return Results.Ok(result);
        });

        group.MapGet("/get-user-by-id/{id:guid}", [Authorize(Policy = "AdminOnly")] async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return Results.Ok(result);
        });

        group.MapGet("/get-user-roles", [Authorize(Policy = "AdminOnly")] async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetAllUserRolesQuery());
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

        group.MapPost("/create-user", [Authorize(Policy = "AdminOnly")] async ([FromBody] CreateUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Created($"/api/users/{result}", Resource.USER_CREATED) : Results.BadRequest(result.Errors);
        });

        group.MapPost("/create-role", [Authorize(Policy = "AdminOnly")] async ([FromBody] CreateRoleCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Created($"/api/users/{result}", Resource.ROLE_CREATED) : Results.BadRequest(result.Errors);
        });

        group.MapPost("/add-user-to-role", [Authorize(Policy = "AdminOnly")] async (CreateUserToRoleCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Created($"/api/users/{result}", Resource.USER_TO_ROLE) : Results.BadRequest(result.Errors);
        });

        group.MapPut("/update-user", [Authorize(Policy = "AdminOnly")] async ([FromBody] UpdateUserCommand command, ISender mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Errors);
        });

        group.MapDelete("/delete-user/{id:guid}", [Authorize(Policy = "AdminOnly")] async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            return result.Succeeded ? Results.NoContent() : Results.NotFound(result.Errors);
        });

        group.MapPatch("/deactive-user/{id:guid}", [Authorize(Policy = "AdminOnly")] async ([FromRoute] Guid id, ISender mediator) =>
        {
            var result = await mediator.Send(new DeactiveUserCommand(id));
            return result.Succeeded ? Results.NoContent() : Results.NotFound(result.Errors);
        });
    }
}
