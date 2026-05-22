using System.Text.Json;
using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Common.Models;

namespace ProjectTaskManagement.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var message = GetMessage(exception);
        var errors = GetErrors(exception);

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception occurred while processing the request.");
        }
        else
        {
            logger.LogWarning(exception, "Request failed with status code {StatusCode}.", statusCode);
        }

        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = ApiResponse<object>.Fail(message, errors);
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ValidationException => StatusCodes.Status400BadRequest,
        ArgumentException => StatusCodes.Status400BadRequest,
        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
        ForbiddenException => StatusCodes.Status403Forbidden,
        NotFoundException => StatusCodes.Status404NotFound,
        ConflictException => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetMessage(Exception exception) => exception switch
    {
        ValidationException => "Validation failed.",
        ArgumentException => exception.Message,
        UnauthorizedAccessException => "Authentication is required to access this resource.",
        ForbiddenException => exception.Message,
        NotFoundException => exception.Message,
        ConflictException => exception.Message,
        _ => "An unexpected error occurred."
    };

    private static IReadOnlyCollection<string>? GetErrors(Exception exception) => exception switch
    {
        ValidationException validationException => validationException.Errors,
        ArgumentException argumentException => [argumentException.Message],
        _ => null
    };
}
