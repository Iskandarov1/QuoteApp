namespace App.Domain.Abstractions;

public class Entity
{
    public readonly List<IDomainEvent> _domainEvents = new();
    public Entity(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; init; }
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}