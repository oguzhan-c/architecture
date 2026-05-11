namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Marks an entity as tenant-aware.
/// The EF Core global query filter automatically scopes all queries
/// to the current tenant by adding <c>WHERE TenantId = @currentTenantId</c>.
/// The EF Core interceptor automatically sets <see cref="TenantId"/>
/// from <c>ICurrentUser</c> on insert.
/// </summary>
public interface IMultiTenant
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant this entity belongs to.
    /// <c>null</c> means the entity is global (not tenant-scoped).
    /// </summary>
    Guid? TenantId { get; set; }
}