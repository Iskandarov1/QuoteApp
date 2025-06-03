using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Entities;

namespace App.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandler(IQuoteRepository quoteRepository)
    : ICommandHandler<CreateQuoteCommand, QuoteResponse>
{
    public async Task<QuoteResponse> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = new Quote(
            Guid.NewGuid(),
            new Author(request.Author),
            new Textt(request.Text),
            new Category(request.Category)
        );

        await quoteRepository.AddAsync(quote, cancellationToken);
        await quoteRepository.SaveChangesAsync(cancellationToken);

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