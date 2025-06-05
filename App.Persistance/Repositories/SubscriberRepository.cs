using App.Domain.Entities.Subscribe;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Repositories;

public class SubscriberRepository(ApplicationDbContext context) : ISubscriberRepository
{
    public async Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Subscribers
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Subscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Subscribers
            .FirstOrDefaultAsync(s => s.Email != null && s.Email.Value == email, cancellationToken);
    }

    public async Task<Subscriber?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var phoneResult = PhoneNumber.Create(phoneNumber);
        if (phoneResult.IsFailure)
            return null;

        var cleaned = phoneResult.Value.Value;
        return await context.Subscribers
            .FirstOrDefaultAsync(s =>
                    s.PhoneNumber != null &&
                    s.PhoneNumber.Value == cleaned,
                cancellationToken);
    }


    public async Task<IEnumerable<Subscriber>> GetActiveSubscribersAsync(CancellationToken cancellationToken = default)
    {
        return await context.Subscribers
            .Where(s => s.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Subscriber>> GetSubscriberForDailyNotificationAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.Date;
        return await context.Subscribers
            .Where(s => s.IsActive &&
                        (s.LastNotificationSent == null || s.LastNotificationSent.Value.Date < today))
            .ToListAsync(cancellationToken);
    }

    /*
    public Task<IEnumerable<Subscriber>> GetSubscribersWithNotificationHistoryAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalSubscribersCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    */

    public async Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
    {
        await context.Subscribers.AddAsync(subscriber, cancellationToken);
    }

    public Task UpdateAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
    {
        context.Subscribers.Update(subscriber);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
    {
        context.Subscribers.Remove(subscriber);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
    
 

}