using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Commands.CreateSubscription;

public record class CreateSubscriptionCommand(
    string? Email,
    string? PhoneNumber
) : ICommand<Maybe<Guid>>;
