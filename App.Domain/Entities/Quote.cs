namespace App.Domain.Entities;
using App.Domain.Abstractions;
public class Quote : Entity
{
    private Quote(): base(Guid.Empty){}
    public Quote(Guid id, Author author, Textt textt, Category category) : base(id)
    {
        Author = author;
        Textt = textt;
        Category = category;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Author Author { get; private set; } = default!;
    public Textt Textt { get; private set; } = default!;
    public Category Category { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    public void Update(Author author, Textt textt, Category category)
    {
        Author = author;
        Textt = textt;
        Category = category;
    }
    
    public static Quote Create(Author author, Textt textt, Category category)
    {
        return new Quote(
            Guid.NewGuid(), 
            author, 
            textt,
            category);
    }

   
    
}