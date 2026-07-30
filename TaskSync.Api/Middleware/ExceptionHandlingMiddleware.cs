using FluentValidation;

namespace TaskSync.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                errors = ex.Errors.Select(x => new
                {
                    x.PropertyName,
                    x.ErrorMessage
                })
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message
            });
        }
    }
}