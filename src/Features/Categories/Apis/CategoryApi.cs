﻿using Autoparts.Api.Features.Categories.CreateCommand;
using Autoparts.Api.Features.Categories.DeleteCommand;
using Autoparts.Api.Features.Categories.GetAllQuery;
using Autoparts.Api.Features.Categories.GetByIdQuery;
using Autoparts.Api.Features.Categories.UpdateCommand;
using Autoparts.Api.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Categories.Apis;

public static class CategoryApi
{

    public static void MapCategoryApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
          .WithTags("Categories");
        //.RequireAuthorization("AdminOnly");
        //.RequireAuthorization();

        group.MapGet("/", GetAll);
        group.MapGet("/{id}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/", Update);
        group.MapDelete("/{id}", Delete);

    }
    private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllCategoriesQuery(pageNumber, pageSize));
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = Resource.RETURN_NULL});
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }

    private static async Task<IResult> Create([FromBody] CreateCategoryCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Created($"/api/categories/{result.ToDictionary()}", result.ToDictionary()) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Update([FromBody] UpdateCategoryCommand command, ISender mediator)
    {
        var result = await mediator.Send(command);
        return result.IsValid ? Results.Ok(result) : Results.BadRequest(result.ToDictionary());
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, ISender mediator)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        return result is true ? Results.NoContent() : Results.NotFound(new { Message = $"{Resource.ID_NOT_FOUND} : {id}" });
    }
}



