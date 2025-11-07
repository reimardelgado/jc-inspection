namespace Shared.Domain.Abstractions;

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