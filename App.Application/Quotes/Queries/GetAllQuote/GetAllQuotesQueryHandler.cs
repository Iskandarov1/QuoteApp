using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;

namespace App.Application.Quotes.Queries.GetAllQuote;

public class GetAllQuotesQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetAllQuotesQuery, Maybe<QuoteListResponse>>
{
    public async Task<Maybe<QuoteListResponse>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetAllAsync(cancellationToken);
        var items = quotes
            .Select(q => new QuoteResponse
            {
                Id = q.Id,
                Author = q.Author.Value,
                Text = q.Textt.Value,
                Category = q.Category.Value,
                CreatedAt = q.CreatedAt
            })
            .ToList();

        if (!items.Any())
            return Maybe<QuoteListResponse>.None;

        var response = new QuoteListResponse
        {
            Quotes = items,
            TotalCount = items.Count
        };

        return Maybe<QuoteListResponse>.From(response);
    }
}