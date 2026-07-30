using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TaskSync.Application.Behaviors;

public sealed class PerformanceBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

    public PerformanceBehavior(
        ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var watch = Stopwatch.StartNew();

        var response = await next();

        watch.Stop();

        if (watch.ElapsedMilliseconds > 500)
        {
            _logger.LogWarning(
                "{Request} took {Elapsed} ms",
                typeof(TRequest).Name,
                watch.ElapsedMilliseconds);
        }

        return response;
    }
}