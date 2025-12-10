using Autoparts.Api.Features.Sales.CreateCommand;
using Autoparts.Api.Features.Sales.DeleteCommand;
using Autoparts.Api.Features.Sales.GetAllQuery;
using Autoparts.Api.Features.Sales.GetByIdQuery;
using Autoparts.Api.Features.Sales.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Sales.Apis;

public static class SaleApi
{
    public static void MapSaleApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sale")
            .WithTags("Sales")
            .RequireAuthorization("SellerOrManager");
        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }

    private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllSalesQuery(pageNumber, pageSize));
        return Results.Ok(result);
    }

    private static async Task<IResult> GetById(ISender mediator, [FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetSaleByIdQuery(id));
        return Results.Ok(result);
    }

    private static async Task<IResult> Create([FromBody] CreateSaleCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Created($"/api/sale/{result.ToDictionary()}", Resource.SALE_CREATED) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Update([FromBody] UpdateSaleCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Delete(ISender mediator, [FromRoute] Guid id)
    {
        var result = await mediator.Send(new DeleteSaleCommand(id));
        return result.IsValid ? Results.NoContent() : Results.NotFound(result.ToDictionary());
    }
}
