using Autoparts.Api.Features.Suppliers.CreateCommand;
using Autoparts.Api.Features.Suppliers.DeleteCommand;
using Autoparts.Api.Features.Suppliers.GetAllQuery;
using Autoparts.Api.Features.Suppliers.GetByIdQuery;
using Autoparts.Api.Features.Suppliers.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Suppliers.Apis;

public static class SupplierApi
{
    public static void MapSupplierApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/supplier")
             .WithTags("Suppliers");

        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }

    private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllSuppliersQuery(pageNumber, pageSize));
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = Resource.RETURN_NULL });
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new GetSupplierByIdQuery(id));
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }

    private static async Task<IResult> Create([FromBody] CreateSupplierCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Created($"/api/supplier/{result.ToDictionary()}", result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Update([FromBody] UpdateSupplierCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new DeleteSupplierCommand(id));
        return result is true ? Results.NoContent() : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
}
