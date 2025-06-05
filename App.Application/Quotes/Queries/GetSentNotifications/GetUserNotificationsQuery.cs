using App.Application.Abstractions.Messaging;
using App.Contracts.Responses.EmailQuoteResponse;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Queries.GetSentNotifications;

public record GetUserNotificationsQuery(
    string? Email,
    string? PhoneNumber
    ): IQuery<Maybe<SentNotificationResponse>>;