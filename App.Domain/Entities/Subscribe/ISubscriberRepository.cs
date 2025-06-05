namespace App.Domain.Entities.Subscribe;

public interface ISubscriberRepository
{
    Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken= default);
    Task<Subscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken= default);
    Task<Subscriber?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken= default);

    Task<IEnumerable<Subscriber>> GetActiveSubscribersAsync(CancellationToken cancellationToken= default);
    Task<IEnumerable<Subscriber>> GetSubscriberForDailyNotificationAsync(CancellationToken cancellationToken= default);
    
    Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken= default);
    Task UpdateAsync(Subscriber subscriber, CancellationToken cancellationToken= default);
    Task DeleteAsync(Subscriber subscriber, CancellationToken cancellationToken= default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    
    // Task<IEnumerable<Subscriber>> GetSubscribersWithNotificationHistoryAsync(int page, int pageSize, CancellationToken cancellationToken);
    // Task<int> GetTotalSubscribersCountAsync(CancellationToken cancellationToken = default);
}