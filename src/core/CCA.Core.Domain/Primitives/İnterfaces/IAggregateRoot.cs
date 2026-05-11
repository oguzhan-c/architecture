using CCA.Core.Domain.Events;

namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Marks an entity as a DDD aggregate root.
/// Aggregate roots are the only entry point to their aggregate cluster.
/// They raise domain events that are dispatched after the transaction commits.
/// </summary>
/// <remarks>
/// Only aggregate roots have repositories. Inner entities of an aggregate
/// are accessed exclusively through the aggregate root.
/// </remarks>
public interface IAggregateRoot
{
    /// <summary>
    /// Gets the list of domain events raised by this aggregate root
    /// since the last time they were cleared.
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears all domain events from this aggregate root.
    /// Called by the dispatcher after events have been published.
    /// </summary>
    void ClearDomainEvents();
}