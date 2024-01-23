namespace Domain.Primitives;

public abstract class Entity
{
    protected Entity(long id) => Id = id;

    protected Entity() { }

    public long Id { get; set; }
}
