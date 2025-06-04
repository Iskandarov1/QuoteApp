namespace App.Contracts.Responses;

public class QuoteListResponse
{
    public IEnumerable<QuoteResponse> Quotes { get; init; } = [];
    public int TotalCount { get; init; }
}