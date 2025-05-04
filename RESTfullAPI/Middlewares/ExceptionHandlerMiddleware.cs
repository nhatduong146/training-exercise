using RESTfullAPI.Exceptions;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace RESTfullAPI.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorizedException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.Unauthorized);
        }
        catch (ForbiddenException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.Forbidden);
        }
        catch (BadRequestException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }
        catch (Exception exception)
        {
            await HandleInternalServerError(context, exception);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode )
    {
        logger.LogError($"Error in ExceptionHandlerMiddleware at {DateTime.Now} " +
            $"with status code {(int)httpStatusCode} and exception {exception}");

        var errorResponse = new JsonErrorResponse(exception.Message);
        if (!env.IsProduction())
        {
            errorResponse.DeveloperMessage = exception.StackTrace;
        }

        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private async Task HandleInternalServerError(HttpContext context, Exception exception)
    {
        logger.LogError($"InternalSeverError in ExceptionHandlerMiddleware at {DateTime.Now} with exception {exception}");

        var errorResponse = new JsonErrorResponse("Something bad happen, please try it later.");
        if (!env.IsProduction())
        {
            errorResponse.Message = exception.Message;
            errorResponse.DeveloperMessage = exception.StackTrace;
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
