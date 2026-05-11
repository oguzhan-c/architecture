using CCA.Core.Domain.Events;
using CCA.Core.Domain.Primitives.Interfaces;

namespace CCA.Core.Domain.Primitives.Classes;

/// <summary>
/// Base class for DDD aggregate roots that are not tenant-scoped.
/// Extends <see cref="Entity{TId}"/> with domain event support.
/// </summary>
/// <remarks>
/// Use this base class for global aggregate roots (not scoped to any tenant).
/// For tenant-scoped aggregate roots, use <see cref="TenantAggregateRoot{TId}"/>.
/// </remarks>
/// <typeparam name="TId">The type of the aggregate root identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <inheritdoc/>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the list of pending events.
    /// Events are dispatched after <c>SaveChangesAsync</c> commits.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    /// <inheritdoc/>
    public void ClearDomainEvents() => _domainEvents.Clear();
}