using App.Domain.Entities;
using App.Domain.Entities.Quots;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Repositories;

public class QuoteRepository(ApplicationDbContext context) : IQuoteRepository
{
    public async Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Quote>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Quotes
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        await context.Quotes.AddAsync(quote, cancellationToken);
    }

    public Task UpdateAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        context.Quotes.Update(quote);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        context.Quotes.Remove(quote);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<int> DeleteQuotesOlderThanAsync(DateTime cutoffDate, CancellationToken cancellationToken = default)
    {
        return await context.Quotes
            .Where(q => q.CreatedAt < cutoffDate)
            .ExecuteDeleteAsync(cancellationToken);
    }
}