using App.Application.Abstractions;
using App.Application.Abstractions.Messaging;
using App.Application.Quotes.Queries.GetRandomQuote;
using App.Domain.Entities.Subscribe;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services;

public class DailyQuoteNotificationWorker(
    ILogger<DailyQuoteNotificationWorker> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    // Change from 30 seconds to 1 minute
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("🔄 Quote notification cycle starting at {Time}", DateTimeOffset.UtcNow);

                await SendDailyQuotesToSubscribers(stoppingToken);
                // if (ShouldSendDailyQuotes())
                // {
                //     
                // }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred during quote notification");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    // private bool ShouldSendDailyQuotes()
    // {
    //     var now = DateTime.UtcNow;
    //     return now.Hour == 9 && now.Minute < 60;
    // }

    private async Task SendDailyQuotesToSubscribers(CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending quotes to all subscribers for demo at {Time}", DateTimeOffset.UtcNow);

        using var scope = serviceProvider.CreateScope();
        var subscriberRepository = scope.ServiceProvider.GetRequiredService<ISubscriberRepository>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var smsService = scope.ServiceProvider.GetRequiredService<ISmsService>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        try
        {
            // For demo purposes, get ALL active subscribers instead of those who 
            // haven't received a notification today
            var subscribers = await subscriberRepository.GetActiveSubscribersAsync(cancellationToken);
            
            if (!subscribers.Any())
            {
                logger.LogInformation("No active subscribers found");
                return;
            }

            var randomQuoteResult = await mediator.Send(new GetRandomQuoteQuery(), cancellationToken);
            
            if (randomQuoteResult.HasNoValue)
            {
                logger.LogWarning("No quotes available for notification");
                return;
            }

            var quote = randomQuoteResult.Value;
            var emailContent = FormatEmailContent(quote.Author, quote.Text, quote.Category);
            var smsContent = FormatSmsContent(quote.Author, quote.Text);

            var successCount = 0;

            foreach (var subscriber in subscribers)
            {
                try
                {
                    if (subscriber.PreferredNotificationMethod == Subscriber.NotificationPreference.Email && subscriber.Email != null)
                    {
                        await emailService.SendEmailAsync(
                            subscriber.Email.Value,
                            "Your Quote of the Minute",
                            emailContent,
                            cancellationToken);
                        logger.LogInformation("📧 Quote sent to {Email}: \"{Quote}\" by {Author}", 
                            subscriber.Email.Value, quote.Text, quote.Author);
                    }
                    else if (subscriber.PreferredNotificationMethod == Subscriber.NotificationPreference.Sms && subscriber.PhoneNumber != null)
                    {
                        await smsService.SendSmsAsync(
                            subscriber.PhoneNumber.Value,
                            smsContent,
                            cancellationToken);
                        logger.LogInformation("📱 Quote sent to {Phone}: \"{Quote}\" by {Author}", 
                            subscriber.PhoneNumber.Value, quote.Text, quote.Author);
                    }

                    subscriber.UpdateNotificationSent(quote.Text, quote.Author);
                    await subscriberRepository.UpdateAsync(subscriber, cancellationToken);
                    successCount++;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send notification to subscriber {SubscriberId}", subscriber.Id);
                }
            }

            await subscriberRepository.SaveChangesAsync(cancellationToken);
            logger.LogInformation("✅ Successfully sent quotes to {Count} subscribers", successCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send quote notifications");
            throw;
        }
    }

    private string FormatEmailContent(string author, string text, string category)
    {
        return $@"
            <html>
            <body>
                <h2>Your Quote of the Minute</h2>
                <blockquote>
                    <p>""{text}""</p>
                    <footer>— {author}</footer>
                </blockquote>
                <p><em>Category: {category}</em></p>
                <hr>
                <p><small>Demo mode: You're receiving quotes every minute for demonstration purposes.</small></p>
            </body>
            </html>";
    }

    private string FormatSmsContent(string author, string text)
    {
        var content = $"\"{text}\" - {author}";
        return content.Length > 150 ? content.Substring(0, 147) + "..." : content;
    }
}