namespace ProgrammingWithPalermo.ChurchBulletin.Core.Model;

public abstract class EntityBase<T> : IEquatable<T> where T : EntityBase<T>, new()
{
    public abstract Guid Id { get; set; }

    public bool Equals(T? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((T)obj);
    }

    public override string ToString()
    {
        return base.ToString() + "-" + Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(EntityBase<T> left, EntityBase<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityBase<T> left, EntityBase<T> right)
    {
        return !Equals(left, right);
    }
}