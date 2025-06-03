using App.Domain.Entities;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

public class EfQuoteRepository : IQuoteRepository
{
    private readonly ApplicationDbContext _context;

    public EfQuoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Quote>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Quotes
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        await _context.Quotes.AddAsync(quote, cancellationToken);
    }

    public Task UpdateAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _context.Quotes.Update(quote);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _context.Quotes.Remove(quote);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}