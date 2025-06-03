using App.Application.Abstractions.Messaging;
using App.Domain.Entities;

namespace App.Application.Quotes.Commands.DeleteQuote;

public class DeleteQuoteCommandHandler(IQuoteRepository quoteRepository) : ICommandHandler<DeleteQuoteCommand, bool>
{
    public async Task<bool> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.QuoteId, cancellationToken);
        
        if (quote == null)
        {
            return false;
        }

        await quoteRepository.DeleteAsync(quote, cancellationToken);
        await quoteRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}