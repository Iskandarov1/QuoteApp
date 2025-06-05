using App.Application.Abstractions.Messaging;
using App.Contracts.Responses.EmailQuoteResponse;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Queries.GetSentNotifications;

public record GetSentNotificationsQuery : IQuery<Maybe<SentNotificationListResponse>>;