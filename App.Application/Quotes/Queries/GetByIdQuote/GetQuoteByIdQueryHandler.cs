using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;

namespace App.Application.Quotes.Queries.GetByIdQuote;

public class GetQuoteByIdQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, Maybe<QuoteResponse>>
{
    public async Task<Maybe<QuoteResponse>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.QuoteId, cancellationToken);
        
        if (quote == null)
        {
            throw new InvalidOperationException($"Quote with ID {request.QuoteId} not found");
        }

        return new QuoteResponse
        {
            Id = quote.Id,
            Author = quote.Author.Value,
            Text = quote.Textt.Value,
            Category = quote.Category.Value,
            CreatedAt = quote.CreatedAt
        };
    }
}