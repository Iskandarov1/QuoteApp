using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities;
using App.Domain.Entities.Quots;

namespace App.Application.Quotes.Commands.DeleteQuote;

public class DeleteQuoteCommandHandler(IQuoteRepository quoteRepository):ICommandHandler<DeleteQuoteCommand, Maybe<Guid>>
{
    public async Task<Maybe<Guid>> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.QuoteId);

        if (quote == null)
        {
            return Maybe<Guid>.None;
        }
        
        await quoteRepository.DeleteAsync(quote, cancellationToken);
        await quoteRepository.SaveChangesAsync(cancellationToken);


        return Maybe<Guid>.From(quote.Id);

    }
}