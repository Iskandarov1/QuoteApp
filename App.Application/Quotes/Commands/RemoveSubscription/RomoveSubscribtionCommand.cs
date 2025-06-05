using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;

namespace App.Application.Quotes.Commands.RemoveSubscription;

public record class RemoveSubscriptionCommand(
    string Email
) : ICommand<Maybe<bool>>;