using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Queries.GetAllQuote;

public record GetAllQuotesQuery : IQuery<Maybe<QuoteListResponse>>;