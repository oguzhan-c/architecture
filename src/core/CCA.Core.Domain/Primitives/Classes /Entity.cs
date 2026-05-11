using CCA.Core.Domain.Primitives.Interfaces;

namespace CCA.Core.Domain.Primitives.Classes;

/// <summary>
/// Base class for all domain entities.
/// Provides identity-based equality — two entities with the same
/// <see cref="Id"/> are considered equal regardless of reference.
/// </summary>
/// <remarks>
/// In DDD, entity identity is determined by the Id, not by memory address.
/// This class enforces that rule through overridden Equals, GetHashCode,
/// and equality operators.
/// </remarks>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> : IEntity<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier of this entity.
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <summary>
    /// Gets or sets the UTC date and time when this entity was created.
    /// Set automatically by the EF Core interceptor on insert.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when this entity was last updated.
    /// Set automatically by the EF Core interceptor on update.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when this entity was soft-deleted.
    /// Null means the entity is not deleted.
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// Determines whether this entity equals another object.
    /// Two entities are equal if they are of the same type and have the same <see cref="Id"/>.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>
    /// <c>true</c> if the objects are equal; otherwise <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <summary>
    /// Returns the hash code for this entity based on its <see cref="Id"/>.
    /// Required when <see cref="Equals(object?)"/> is overridden — ensures
    /// entities work correctly in dictionaries and hash sets.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode() => Id!.GetHashCode();

    /// <summary>
    /// Determines whether two entities are equal using identity-based comparison.
    /// </summary>
    public static bool operator ==(Entity<TId>? a, Entity<TId>? b) => Equals(a, b);

    /// <summary>
    /// Determines whether two entities are not equal using identity-based comparison.
    /// </summary>
    public static bool operator !=(Entity<TId>? a, Entity<TId>? b) => !Equals(a, b);
}