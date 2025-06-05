using App.Api.Contracts;
using App.Application.Quotes.Commands.CreateQuote;
using App.Application.Quotes.Commands.CreateSubscription;
using App.Application.Quotes.Commands.RemoveSubscription;
using App.Application.Quotes.Queries.GetSentNotifications;
using App.Contracts.Requests;
using App.Contracts.Responses;
using App.Contracts.Responses.EmailQuoteResponse;
using App.Domain.Core.Primitives.Maybe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller;

[ApiController]
[Route("[controller]")]
public class SubscriptionController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost(ApiRoutes.Subscriptions.Subscribe)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionRequest request) =>
        await Maybe<CreateSubscriptionCommand>
            .From(new CreateSubscriptionCommand(request.Email, request.PhoneNumber))
            .Bind(command => Mediator.Send(command))
            .Match(id => Ok(id), BadRequest);

    [HttpDelete(ApiRoutes.Subscriptions.Unsubscribe)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Unsubscribe([FromBody] RemoveSubscriptionRequest request, CancellationToken cancellationToken) =>
        await Maybe<RemoveSubscriptionCommand>
            .From(new RemoveSubscriptionCommand(request.Email!))
            .Bind(command => Mediator.Send(command, cancellationToken))
            .Match(result => Ok(result), BadRequest);

    [HttpGet(ApiRoutes.Subscriptions.GetSentNotification)]
    [ProducesResponseType(typeof(SentNotificationListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSentNotifications() =>
        await Maybe<GetSentNotificationsQuery>
            .From(new GetSentNotificationsQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HttpGet("my-notifications")]
    [ProducesResponseType(typeof(SentNotificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetMyNotifications(
        [FromQuery] string? email,
        [FromQuery] string? phoneNumber) =>
        await Maybe<GetUserNotificationsQuery>
            .From(new GetUserNotificationsQuery(email, phoneNumber))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
}