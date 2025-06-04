using App.Api.Contracts;
using App.Application.Quotes.Commands.CreateQuote;
using App.Application.Quotes.Commands.DeleteQuote;
using App.Application.Quotes.Commands.UpdateQuote;
using App.Contracts.Responses;
using App.Application.Quotes.Queries.GetAllQuote;
using App.Application.Quotes.Queries.GetByIdQuote;
using App.Application.Quotes.Queries.GetRandomQuote;
using App.Contracts.Requests;
using App.Domain.Abstractions;
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
    public async Task<IActionResult> GetById(Guid quoteId) =>
        await Maybe<GetQuoteByIdQuery>
            .From(new GetQuoteByIdQuery(quoteId))
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
    
    [HttpPut(ApiRoutes.Quotes.Update)]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid quoteId, [FromBody] UpdateQuoteRequest request) =>
        await Maybe<UpdateQuoteCommand>
            .From(new UpdateQuoteCommand(quoteId, request.Author, request.Text, request.Category))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Quotes.Create)]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuoteRequest request) =>
        await Maybe<CreateQuoteCommand>
            .From(new CreateQuoteCommand(request.Author, request.Text, request.Category))
            .Bind(command => Mediator.Send(command))
            .Match(id =>Ok(id), BadRequest);

    [HttpDelete(ApiRoutes.Quotes.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Remove(Guid quoteId) =>
        await Maybe<DeleteQuoteCommand>
            .From(new DeleteQuoteCommand(quoteId))
            .Bind(command => Mediator.Send(command))
            .Match(id => Ok(id), BadRequest);
    
    [HttpGet(ApiRoutes.Quotes.Random)]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRandom() =>
        await Maybe<GetRandomQuoteQuery>
            .From(new GetRandomQuoteQuery())
            .Bind(q => Mediator.Send(q))
            .Match(Ok, NotFound);
    
    
    
    
    
}