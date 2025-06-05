namespace App.Domain.Entities.Quots;

public interface IQuoteRepository
{
     Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Quote>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Quote quote, CancellationToken cancellationToken = default);
    Task UpdateAsync(Quote quote, CancellationToken cancellationToken = default);
    Task DeleteAsync(Quote quote, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> DeleteQuotesOlderThanAsync(DateTime cutoffDate, CancellationToken cancellationToken = default);

}