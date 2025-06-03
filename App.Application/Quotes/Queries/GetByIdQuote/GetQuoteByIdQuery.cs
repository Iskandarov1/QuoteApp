using System.ComponentModel.DataAnnotations;
using App.Application.Abstractions.Messaging;
using App.Contracts.Responses;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Queries.GetByIdQuote;

public record GetQuoteByIdQuery([property: Required] Guid QuoteId) : IQuery<Maybe<QuoteResponse>>;