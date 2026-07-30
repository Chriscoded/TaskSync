using Microsoft.Extensions.Logging;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            """
            -----------------------------
            TO: {To}
            SUBJECT: {Subject}

            {Body}
            -----------------------------
            """,
            to,
            subject,
            body);

        return Task.CompletedTask;
    }
}