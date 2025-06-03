using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Commands.UpdateQuote;

public record UpdateQuoteCommand(
    Guid Id, 
    string Author,
    string Text,
    string Category)
    :  ICommand<Maybe<QuoteResponse>>;


