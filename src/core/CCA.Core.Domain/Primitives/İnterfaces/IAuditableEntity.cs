namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Marks an entity as auditable.
/// The EF Core interceptor automatically sets <see cref="CreatedBy"/>
/// and <see cref="UpdatedBy"/> from <c>ICurrentUser</c> on every save.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who created this entity.
    /// Set automatically on insert.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this entity.
    /// Set automatically on update.
    /// </summary>
    string? UpdatedBy { get; set; }
}