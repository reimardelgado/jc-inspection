using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Application.Utils;
using Shared.Domain.Exceptions;

namespace Shared.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "{Exception} - {Code} - {Message}", nameof(exception), exception.GetHashCode(),
            exception.Message);

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            DomainException domainException => (int)(domainException.HttpStatusCode ?? HttpStatusCode.BadRequest),
            ValidationException _ => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var response = JsonSerializer.Serialize(new
        {
            RequestId = context.TraceIdentifier,
            Code = exception.GetHashCode(),
            Errors = ProcessResponseUtils.GetMessageError(exception)
        }, serializeOptions);

        await context.Response.WriteAsync(response);
    }
}