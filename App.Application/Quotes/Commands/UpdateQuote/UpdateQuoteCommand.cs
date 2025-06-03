using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;

namespace App.Application.Quotes.Commands.UpdateQuote;

public record UpdateQuoteCommand(
    Guid Id,
    string Author,
    string Text,
    string Category
) : ICommand<QuoteResponse>;