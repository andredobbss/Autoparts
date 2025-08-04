using Autoparts.Api.Features.Sales.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Api.Features.Sales.Apis;

public static class SaleApi
{
    public static void MapSaleApi(this IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup("/api/sale")
        //    .WithTags("Sales");
        //group.MapGet("/", GetAll);
        //group.MapGet("/{id}", GetById);
        //group.MapPost("/", Create);
        //group.MapPut("/", Update);
        //group.MapDelete("/{id}", Delete);
    }

    //private static async Task<IResult> GetAll(ISender mediator, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    //{
    //    var result = await mediator.Send(new GetAllSalesQuery(pageNumber, pageSize));
    //    return result is not null ? Results.Ok(result) : Results.NotFound(new { Message = Resource.SALE_NULL });
    //}
}
