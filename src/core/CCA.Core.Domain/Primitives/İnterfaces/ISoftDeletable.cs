namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Marks an entity as soft-deletable.
/// Soft-deleted entities are never physically removed from the database.
/// The EF Core global query filter automatically excludes them from all queries
/// by adding <c>WHERE DeletedAt IS NULL</c>.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets the UTC date and time when this entity was soft-deleted.
    /// <c>null</c> means the entity is active and not deleted.
    /// </summary>
    DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who deleted this entity.
    /// </summary>
    Guid? DeletedByUserId { get; set; }

    /// <summary>
    /// Gets a value indicating whether this entity has been soft-deleted.
    /// </summary>
    bool IsDeleted { get; }
}