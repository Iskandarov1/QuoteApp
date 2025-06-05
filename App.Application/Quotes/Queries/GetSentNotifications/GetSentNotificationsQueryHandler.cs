using App.Application.Abstractions.Messaging;
using App.Contracts.Responses.EmailQuoteResponse;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities.Subscribe;

namespace App.Application.Quotes.Queries.GetSentNotifications;

public class GetSentNotificationsQueryHandler(ISubscriberRepository subscriberRepository) : IQueryHandler<GetSentNotificationsQuery, Maybe<SentNotificationListResponse>>
{
    public async Task<Maybe<SentNotificationListResponse>> Handle(GetSentNotificationsQuery request, CancellationToken cancellationToken)
    {
        var subscribers = await subscriberRepository.GetActiveSubscribersAsync(cancellationToken);

        var notifiedSubscribers = subscribers
            .Where(s => s.LastNotificationSent.HasValue)
            .OrderByDescending(s => s.LastNotificationSent);

        var notifications = notifiedSubscribers
            .Select(s => new SentNotificationResponse(
                s.Id,
                s.Email?.Value,
                s.PhoneNumber?.Value,
                s.PreferredNotificationMethod.ToString(),
                s.LastSentQuoteText,
                s.LastSentQuoteAuthor,
                s.LastNotificationSent,
                s.IsActive));

        if (!notifications.Any())
        {
            return Maybe<SentNotificationListResponse>.None;
        }

        var response = new SentNotificationListResponse
        {
            Notifications = notifications,
            Count = notifications.Count()
        };

        return Maybe<SentNotificationListResponse>.From(response);







    }
}