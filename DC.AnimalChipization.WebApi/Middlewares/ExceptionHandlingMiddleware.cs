using System;
using System.Threading.Tasks;
using DC.AnimalChipization.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DC.AnimalChipization.WebApi.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            HandleException(context, e);
        }
    }

    private void HandleException(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ValidationException   => StatusCodes.Status400BadRequest,
        UnauthorizedException => StatusCodes.Status401Unauthorized,
        AccessDeniedException => StatusCodes.Status403Forbidden,
        NotFoundException     => StatusCodes.Status404NotFound,
        ConflictException     => StatusCodes.Status409Conflict,
        _                     => StatusCodes.Status500InternalServerError
    };
}