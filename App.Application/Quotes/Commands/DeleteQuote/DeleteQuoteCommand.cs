using App.Application.Abstractions.Messaging;

namespace App.Application.Quotes.Commands.DeleteQuote;

public abstract record DeleteQuoteCommand(Guid QuoteId) : ICommand<bool>;