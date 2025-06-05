using App.Application.Abstractions;
using App.Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Services;

public class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Mock Email sent to {Email} with subject: {Subject}", to, subject);
        logger.LogDebug("Email body: {Body}", body);
        return Task.CompletedTask;
    }
}