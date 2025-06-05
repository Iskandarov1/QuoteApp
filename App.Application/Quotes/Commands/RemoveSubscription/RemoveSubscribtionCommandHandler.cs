using App.Application.Abstractions.Messaging;
using App.Domain.Core.Primitives.Maybe;
using App.Domain.Entities.Subscribe;

namespace App.Application.Quotes.Commands.RemoveSubscription;

public  class RemoveSubscriptionCommandHandler(ISubscriberRepository subscriberRepository):
    ICommandHandler<RemoveSubscriptionCommand, Maybe<bool>> 
    {
        public async Task<Maybe<bool>> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var getEmailRep = await subscriberRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (getEmailRep != null) await subscriberRepository.DeleteAsync(getEmailRep, cancellationToken);

            await subscriberRepository.SaveChangesAsync(cancellationToken);

            return Maybe<bool>.From(true);
        }
    }