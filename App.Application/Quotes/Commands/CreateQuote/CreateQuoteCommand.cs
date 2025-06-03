using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;

namespace App.Application.Quotes.Commands.CreateQuote;

public record CreateQuoteCommand(
    string Author,
    string Text,
    string Category
) : ICommand<QuoteResponse>;