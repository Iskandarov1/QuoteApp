using App.Api.Contracts;
using App.Application.Quotes.Commands.CreateQuote;
using App.Application.Quotes.Commands.UpdateQuote;
using App.Contracts.Responses;
using App.Application.Quotes.Queries.GetAllQuote;
using App.Application.Quotes.Queries.GetByIdQuote;
using App.Contracts.Requests;
using App.Domain.Abstractions;
using App.Domain.Core.Errors;
using App.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Maybe;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
            .Match(id =>Ok(id), () => BadRequest());

}