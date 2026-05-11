using CCA.Core.Domain.Primitives.Interfaces;

namespace CCA.Core.Domain.Primitives.Classes;

/// <summary>
/// Base class for tenant-scoped domain entities that do not raise domain events.
/// Extends <see cref="Entity{TId}"/> with <see cref="IMultiTenant"/> support.
/// </summary>
/// <remarks>
/// Use this base class for entities that:
/// <list type="bullet">
///   <item>Belong to a specific tenant</item>
///   <item>Do not raise domain events</item>
///   <item>Live inside an aggregate (not an aggregate root themselves)</item>
/// </list>
/// For entities that raise domain events, use <see cref="TenantAggregateRoot{TId}"/>.
/// For global (non-tenant) entities, use <see cref="Entity{TId}"/>.
/// </remarks>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class TenantEntity<TId> : Entity<TId>, IMultiTenant
{
    /// <inheritdoc/>
    public Guid? TenantId { get; set; }
}