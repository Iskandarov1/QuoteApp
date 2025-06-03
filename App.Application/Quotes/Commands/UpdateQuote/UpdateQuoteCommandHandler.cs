using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;

namespace App.Application.Quotes.Commands.UpdateQuote;

public class UpdateQuoteCommandHandler(IQuoteRepository quoteRepository) : ICommandHandler<UpdateQuoteCommand, Maybe<QuoteResponse>>
{
    public async Task<Maybe<QuoteResponse>> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
    {
        //got the quote id
        var quote = await quoteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (quote==null)
        {
            throw new InvalidOperationException($"failed to get the quote with id {request.Id} ");
        }

        quote.Update(
            new Author(request.Author),
            new Textt(request.Text),
            new Category(request.Category)
        );

        await quoteRepository.UpdateAsync(quote, cancellationToken);
        await quoteRepository.SaveChangesAsync(cancellationToken);

        return new QuoteResponse
        {
            Id = quote.Id,
            Author = quote.Author.Value,
            Category = quote.Category.Value,
            CreatedAt = quote.CreatedAt
        };


    }
}
