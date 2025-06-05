using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities.Subscribe;

namespace App.Application.Quotes.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(ISubscriberRepository subscriberRepository)
    :ICommandHandler<CreateSubscriptionCommand, Maybe<Guid>>
{
    public async Task<Maybe<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            
            var emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Maybe<Guid>.None;
            }
            var existingSubscriber =
                await subscriberRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingSubscriber !=null)
            {
                return Maybe<Guid>.None;
            }
            var subscriber = Subscriber.CreateWithEmail(emailResult.Value);
            await subscriberRepository.AddAsync(subscriber, cancellationToken);
            await subscriberRepository.SaveChangesAsync(cancellationToken);
            
            return Maybe<Guid>.From(subscriber.Id);
            
        }
        
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            var phoneResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneResult.IsFailure)
            {
               return Maybe<Guid>.None;
            }
            var existingSubscriber = 
                await subscriberRepository.GetByPhoneNumber(request.PhoneNumber, cancellationToken);
            if (existingSubscriber != null)
            {
                return Maybe<Guid>.None;
            }
            var subscriber = Subscriber.CreateWithPhoneNumber(phoneResult.Value);

            await subscriberRepository.AddAsync(subscriber, cancellationToken);
            await subscriberRepository.SaveChangesAsync(cancellationToken);

            return Maybe<Guid>.From(subscriber.Id);
        }
        
        return Maybe<Guid>.None;
    }
}