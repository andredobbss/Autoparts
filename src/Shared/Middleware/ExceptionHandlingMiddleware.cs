using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace Autoparts.Api.Shared.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Resource not found.");
            await HandleExceptionAsync(context, ex.Message, "KeyNotFoundException", HttpStatusCode.NotFound);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid argument.");
            await HandleExceptionAsync(context, ex.Message, "ArgumentException", HttpStatusCode.BadRequest);
        }
        catch(InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Invalid operation.");
            await HandleExceptionAsync(context, ex.Message, "InvalidOperationException", HttpStatusCode.BadRequest);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "FluentValidation error.");
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            await HandleExceptionAsync(context, errors, "FluentValidationException", HttpStatusCode.BadRequest);
        }
        catch (FileNotFoundException ex)
        {
            logger.LogWarning(ex, "File not found.");
            await HandleExceptionAsync(context, ex.Message, "FileNotFoundException", HttpStatusCode.NotFound);
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Unauthorized access.");
            await HandleExceptionAsync(context, ex.Message, "UnauthorizedAccessException", HttpStatusCode.Unauthorized);
        }
        catch (SqlException ex)
        {
            logger.LogError(ex, "Database error.");
            await HandleExceptionAsync(context, ex.Message, "SqlException", HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error.");
            await HandleExceptionAsync(context, ex.Message, "Exception", HttpStatusCode.InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, object error, string type, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            type,
            error,
            detail = statusCode.ToString()
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
