using Autoparts.Api.Features.Returns.CreateCommand;
using Autoparts.Api.Features.Returns.DeleteCommand;
using Autoparts.Api.Features.Returns.GetAllQuery;
using Autoparts.Api.Features.Returns.GetByIdQuery;
using Autoparts.Api.Features.Returns.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Returns.Apis;

public static class ReturnApi
{
    public static void MapReturnApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/return")
             .WithTags("Returns")
             .RequireAuthorization("ManagerOnly");
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }

    private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllReturnsQuery(pageNumber, pageSize));
        return Results.Ok(result);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new GetReturnByIdQuery(id));
        return Results.Ok(result);
    }

    private static async Task<IResult> Create([FromBody] CreateReturnCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Created($"/api/return/{result.ToDictionary()}", Resource.RETURN_CREATED) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Update([FromBody] UpdateReturnCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Ok(result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new DeleteReturnCommand(id));
        return result.IsValid ? Results.NoContent() : Results.NotFound(result.ToDictionary());
    }
}
