using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;
using App.Domain.Entities.Quots;

namespace App.Application.Quotes.Queries.GetByIdQuote;

public class GetQuoteByIdQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, Maybe<QuoteResponse>>
{
    public async Task<Maybe<QuoteResponse>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.QuoteId, cancellationToken);

        if (quote != null)
        {
            var response = new QuoteResponse(
                quote.Id,
                quote.Author.Value, 
                quote.Textt.Value,
                quote.Category.Value)
            { CreatedAt = quote.CreatedAt};

            return Maybe<QuoteResponse>.From(response);
        }
        else
        {
            return Maybe<QuoteResponse>.None;
        }
    }
}