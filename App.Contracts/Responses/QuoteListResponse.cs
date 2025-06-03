namespace App.Contracts.Responses;

public class QuoteListResponse
{
    public IEnumerable<QuoteResponse> Quotes { get; init; } = Enumerable.Empty<QuoteResponse>();
    public int TotalCount { get; init; }
}