namespace App.Contracts.Requests;

public class CreateQuoteRequest
{
    public string Author { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
}