using App.Application.Abstractions;
using App.Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Services;

public class SmsService(ILogger<SmsService> logger) : ISmsService
{
    public Task SendSmsAsync(string to, string message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Mock SMS sent to {PhoneNumber}: {Message}", to, message);
        return Task.CompletedTask;
    }
}