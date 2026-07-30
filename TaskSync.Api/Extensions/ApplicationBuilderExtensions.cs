using TaskSync.Api.Middleware;

namespace TaskSync.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationMiddleware(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}