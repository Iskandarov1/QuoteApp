using App.Application.Abstractions.Messaging;
using App.Contracts.Responses.EmailQuoteResponse;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities.Subscribe;

namespace App.Application.Quotes.Queries.GetSentNotifications;

public class GetUserNotificationsQueryHandler(ISubscriberRepository subscriberRepository): IQueryHandler<GetUserNotificationsQuery, Maybe<SentNotificationResponse>>
{
    public async Task<Maybe<SentNotificationResponse>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        Subscriber? subscriber = null;
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            subscriber = await subscriberRepository.GetByEmailAsync(request.Email, cancellationToken);
            Console.WriteLine($"Searched by email {request.Email}, found subscribers : {subscriber?.Id}");
        }

        if (subscriber == null && !string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            subscriber = await subscriberRepository.GetByPhoneNumber(request.PhoneNumber, cancellationToken);
            Console.WriteLine($"Searched by phone: {request.PhoneNumber}, found subscribers: {subscriber?.Id}");
        }

        if (subscriber==null || !subscriber.LastNotificationSent.HasValue)
        {
            return Maybe<SentNotificationResponse>.None;
        }
        
        var response  = new SentNotificationResponse(
            subscriber.Id,
            subscriber.Email?.Value,
            subscriber.PhoneNumber?.Value,
            subscriber.PreferredNotificationMethod.ToString(),
            subscriber.LastSentQuoteText,
            subscriber.LastSentQuoteAuthor,
            subscriber.LastNotificationSent,
            subscriber.IsActive);
            
            
            return Maybe<SentNotificationResponse>.From(response);
            
            
        
        
        
    }
}