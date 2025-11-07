namespace Backend.Domain.Entities;

// This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
// Using non-generic integer types for simplicity and to ease caching logic
public class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; private init; }
    public DateTime? UpdatedAt { get; protected set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public BaseEntity(Guid id = new())
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }
}