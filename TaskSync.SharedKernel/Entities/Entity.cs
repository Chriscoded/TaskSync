namespace TaskSync.SharedKernel.Entities;

public abstract class Entity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; protected set; } = default!;

    protected Entity() { }

    protected Entity(TKey id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TKey> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
        => HashCode.Combine(GetType(), Id);

    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right)
        => Equals(left, right);

    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right)
        => !(left == right);
}