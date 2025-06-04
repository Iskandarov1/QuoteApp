using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities.Subscribe;

namespace App.Application.Quotes.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(ISubscriberRepository subscriberRepository):ICommandHandler<CreateSubscriptionCommand, Maybe<Guid>>
{
    public async Task<Maybe<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var emailResult = Email.Create(request.Email);
            var existingSubscriber =
                await subscriberRepository.GetByEmailAsync(request.Email, cancellationToken);
            var subscriber = Subscriber.CreateWithEmail(emailResult.Value);

            return Maybe<Guid>.From(subscriber.Id);

        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            var phoneResult = PhoneNumber.Create(request.PhoneNumber);
            var existingSubscriber = await subscriberRepository.GetByPhoneNumber(request.PhoneNumber);
            var subscriber = Subscriber.CreateWithPhoneNumber(phoneResult.Value);

            return Maybe<Guid>.From(subscriber.Id);
        }

        return Maybe<Guid>.None;
    }
}