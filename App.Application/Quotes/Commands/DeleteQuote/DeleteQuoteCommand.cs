using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Commands.DeleteQuote;

public record DeleteQuoteCommand(Guid QuoteId) :  ICommand<Maybe<Guid>>;