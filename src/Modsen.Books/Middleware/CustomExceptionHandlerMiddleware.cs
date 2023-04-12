using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Models;

namespace Modsen.Books.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Message);
                break;
            case NotFoundException<Book>:
                code = HttpStatusCode.NotFound;
                result = "Book was not found";
                break;
            case NotFoundException<Author>:
                code = HttpStatusCode.NotFound;
                result = "Author was not found";
                break;
        }

        context.Request.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
            result = JsonSerializer.Serialize(new { error = exception.Message });

        return context.Response.WriteAsync(result);
    }
}