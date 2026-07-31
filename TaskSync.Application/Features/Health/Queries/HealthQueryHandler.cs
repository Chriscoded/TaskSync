using MediatR;

namespace TaskSync.Application.Features.Health;

public sealed class HealthQueryHandler
    : IRequestHandler<HealthQuery, HealthResponse>
{
    public Task<HealthResponse> Handle(
        HealthQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            new HealthResponse(
                "Healthy",
                DateTime.UtcNow));
    }
}