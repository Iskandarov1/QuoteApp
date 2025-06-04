namespace App.Contracts.Requests;

public class CreateSubscriptionRequest
{
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}