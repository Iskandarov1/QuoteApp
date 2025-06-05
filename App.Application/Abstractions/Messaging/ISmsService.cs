namespace App.Application.Abstractions.Messaging;

public interface ISmsService
{
    Task SendSmsAsync(string to, string message, CancellationToken cancellationToken = default);
}