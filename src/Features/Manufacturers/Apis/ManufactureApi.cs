using Autoparts.Api.Features.Manufacturers.CreateCommand;
using Autoparts.Api.Features.Manufacturers.DeleteCommand;
using Autoparts.Api.Features.Manufacturers.GetAllQuery;
using Autoparts.Api.Features.Manufacturers.GetByIdQuery;
using Autoparts.Api.Features.Manufacturers.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Manufacturers.Apis;

public static class ManufactureApi
{
    public static void MapManufactureApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/manufacturers")
            .WithTags("Manufacturers");
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }
    private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllManufacturersQuery(pageNumber, pageSize));
        return Results.Ok(result);
    }
    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new GetManufacturerByIdQuery(id));
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
    private static async Task<IResult> Create([FromBody] CreateManufacturerCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Created($"/api/manufacturers/{result.ToDictionary()}", result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }
    private static async Task<IResult> Update([FromBody] UpdateManufacturerCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }
    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new DeleteManufacturerCommand(id));
        return result is true ? Results.NoContent() : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
}
