using MediatR;

namespace TaskSync.Application.Features.Health;

public sealed record HealthQuery : IRequest<HealthResponse>;

public sealed record HealthResponse(
    string Status,
    DateTime UtcTime);