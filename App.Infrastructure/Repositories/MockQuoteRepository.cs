using App.Domain.Entities;

namespace App.Infrastructure.Repositories;

public class MockQuoteRepository : IQuoteRepository
{
    private readonly List<Quote> _quotes;

    public MockQuoteRepository()
    {
        _quotes = new List<Quote>
        {
            new Quote(
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                new Author("Albert Einstein"),
                new Textt("Imagination is more important than knowledge."),
                new Category("Science")
            ),
            new Quote(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                new Author("Maya Angelou"),
                new Textt("You will face many defeats in life, but never let yourself be defeated."),
                new Category("Inspiration")
            ),
            new Quote(
                Guid.Parse("33333333-3333-3333-3333-333333333333"),
                new Author("Steve Jobs"),
                new Textt("Innovation distinguishes between a leader and a follower."),
                new Category("Business")
            ),
            new Quote(
                Guid.Parse("44444444-4444-4444-4444-444444444444"),
                new Author("Winston Churchill"),
                new Textt("Success is not final, failure is not fatal: it is the courage to continue that counts."),
                new Category("Leadership")
            )
        };
    }

    public Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var quote = _quotes.FirstOrDefault(q => q.Id == id);
        return Task.FromResult(quote);
    }

    public Task<IEnumerable<Quote>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<Quote>>(_quotes);
    }

    public Task AddAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _quotes.Add(quote);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        var existingQuote = _quotes.FirstOrDefault(q => q.Id == quote.Id);
        if (existingQuote != null)
        {
            var index = _quotes.IndexOf(existingQuote);
            _quotes[index] = quote;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _quotes.Remove(quote);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // No-op for mock implementation
        return Task.CompletedTask;
    }
}