using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Queries.GetRandomQuote;

public record GetRandomQuoteQuery : IQuery<Maybe<QuoteResponse>>;