using App.Api.Contracts;
using App.Contracts.Responses;
using App.Application.Quotes.Queries.GetAllQuote;
using App.Application.Quotes.Queries.GetByIdQuote;
using App.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Maybe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller;

[ApiController]
[Route("[controller]")]
public class QuoteController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet(ApiRoutes.Quotes.GetById)]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid Id) =>
        await Maybe<GetQuoteByIdQuery>
            .From(new GetQuoteByIdQuery(Id))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Quotes.GetAll)]
    [ProducesResponseType(typeof(QuoteListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll() =>
        await Maybe<GetAllQuotesQuery>
            .From(new GetAllQuotesQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
    
    
    
    
}