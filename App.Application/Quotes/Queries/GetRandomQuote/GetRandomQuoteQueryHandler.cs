using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;
using App.Domain.Entities.Quots;

namespace App.Application.Quotes.Queries.GetRandomQuote;

public class GetRandomQuoteQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuoteQuery, Maybe<QuoteResponse>>
{
    public async Task<Maybe<QuoteResponse>> Handle(GetRandomQuoteQuery request, CancellationToken cancellationToken)
    {
        var allQuotes = (await quoteRepository.GetAllAsync(cancellationToken)).ToList();

        if (!allQuotes.Any())
        {
            return Maybe<QuoteResponse>.None;
        }
        var randomIndex = new Random().Next(allQuotes.Count);
        var q = allQuotes[randomIndex];


        var response = new QuoteResponse(
            q.Id,
            q.Author.Value,
            q.Textt.Value,
            q.Category.Value);

        return Maybe<QuoteResponse>.From(response);
    }
}