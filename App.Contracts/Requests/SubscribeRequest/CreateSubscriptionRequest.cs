namespace App.Contracts.Requests.SubscribeRequest;

public class CreateSubscriptionRequest
{
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}