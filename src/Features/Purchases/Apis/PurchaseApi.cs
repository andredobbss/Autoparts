using Autoparts.Api.Features.Purchases.CreateCommand;
using Autoparts.Api.Features.Purchases.DeleteCommand;
using Autoparts.Api.Features.Purchases.GetAllQuery;
using Autoparts.Api.Features.Purchases.GetByIdQuery;
using Autoparts.Api.Features.Purchases.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Purchases.Apis;

public static class PurchaseApi
{
    public static void MapPurchaseApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase")
            .WithTags("Purchases");
        group.MapGet("/", GetAll);
        group.MapGet("/GetAllIPurchaseProductCategoryManufaturer", GetAllIPurchaseProductCategoryManufaturer);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);
    }

    private static async Task<IResult> GetAll(ISender mediator, CancellationToken cancellationToken, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllPurchasesQuery(pageNumber, pageSize), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetAllIPurchaseProductCategoryManufaturer(ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPurchaseProductCategoryManufaturerQuery(),cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPurchaseByIdQuery(id),cancellationToken);
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }

    private static async Task<IResult> Create([FromBody] CreatePurchaseCommand command, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.IsValid ? Results.Created($"/api/purchase/{result.ToDictionary()}", result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Update([FromBody] UpdatePurchaseCommand command, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeletePurchaseCommand(id), cancellationToken);
        return result is true ? Results.NoContent() : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
}
