using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Abstractions;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;

namespace App.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandler(IQuoteRepository quoteRepository)
    : ICommandHandler<CreateQuoteCommand, Maybe<Guid>>
{
    public async Task<Maybe<Guid>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote =  Quote.Create(
            new Author(request.Author),
            new Textt(request.Text),
            new Category(request.Category)
        );

        await quoteRepository.AddAsync(quote, cancellationToken);
        await quoteRepository.SaveChangesAsync(cancellationToken);


        return Maybe<Guid>.From(quote.Id);
    }
}