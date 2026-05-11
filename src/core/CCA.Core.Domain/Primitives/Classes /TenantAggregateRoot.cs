namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Base class for tenant-scoped DDD aggregate roots.
/// Combines <see cref="AggregateRoot{TId}"/> with <see cref="IMultiTenant"/> support.
/// </summary>
/// <remarks>
/// This is the most common base class for top-level domain concepts in
/// multi-tenant applications built on CCA. Examples: Order, Invoice, Product.
/// <para>
/// Use <see cref="AggregateRoot{TId}"/> for global aggregates (not tenant-scoped).
/// Use <see cref="TenantEntity{TId}"/> for inner entities that live inside an aggregate.
/// </para>
/// </remarks>
/// <typeparam name="TId">The type of the aggregate root identifier.</typeparam>
public abstract class TenantAggregateRoot<TId> : AggregateRoot<TId>, IMultiTenant
{
    /// <inheritdoc/>
    public Guid? TenantId { get; set; }
}