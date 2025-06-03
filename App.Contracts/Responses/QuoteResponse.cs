namespace App.Contracts.Responses;

public sealed record QuoteResponse()
{
    public Guid Id { get; init; }
    public string Author { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}