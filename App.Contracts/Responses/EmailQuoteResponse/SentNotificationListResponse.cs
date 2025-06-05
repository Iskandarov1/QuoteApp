namespace  App.Contracts.Responses.EmailQuoteResponse;

public class SentNotificationListResponse
{
    public IEnumerable<SentNotificationResponse> Notifications { get; init; } = [];
    public int Count { get; init; }
}