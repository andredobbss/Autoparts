using Autoparts.Api.Features.Clients.CreateCommand;
using Autoparts.Api.Features.Clients.DeleteCommand;
using Autoparts.Api.Features.Clients.GetAllQuery;
using Autoparts.Api.Features.Clients.GetByIdQuery;
using Autoparts.Api.Features.Clients.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Clients.Apis;

public static class ClientApi
{
    public static void MapClientApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clients")
            .WithTags("Clients");
        //.RequireAuthorization("AdminOnly");
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }
    private static async Task<IResult> GetAll(ISender mediator, CancellationToken cancellationToken, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllClientsQuery(pageNumber, pageSize), cancellationToken);
        return Results.Ok(result);
    }
    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetClientByIdQuery(id), cancellationToken);
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
    private static async Task<IResult> Create([FromBody] CreateClientCommand command, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.IsValid ? Results.Created($"/api/clients/{result.ToDictionary()}", result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }
    private static async Task<IResult> Update([FromBody] UpdateClientCommand command, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }
    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteClientCommand(id), cancellationToken);
        return result is true ? Results.NoContent() : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
}
