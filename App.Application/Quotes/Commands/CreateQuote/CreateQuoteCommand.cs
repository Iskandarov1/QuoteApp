using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Commands.CreateQuote;

public record CreateQuoteCommand(
    string Author,
    string Text,
    string Category
) : ICommand<Maybe<Guid>>;