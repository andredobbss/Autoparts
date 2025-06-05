using Autoparts.Api.Shared.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Autoparts.Api.Shared.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainValidationException ex)
        {
            _logger.LogWarning(ex, "Domain validation error.");
            await HandleExceptionAsync(context, ex.Message, "DomainValidationException", HttpStatusCode.BadRequest);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "FluentValidation error.");
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            await HandleExceptionAsync(context, errors, "FluentValidationException", HttpStatusCode.BadRequest);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access.");
            await HandleExceptionAsync(context, ex.Message, "UnauthorizedAccessException", HttpStatusCode.Unauthorized);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");
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
