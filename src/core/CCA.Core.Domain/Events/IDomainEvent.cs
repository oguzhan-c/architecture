namespace CCA.Core.Domain.Events;

/// <summary>
/// Marker interface for domain events.
/// Domain events represent something meaningful that happened in the domain.
/// They are raised inside aggregate roots and dispatched after
/// <c>SaveChangesAsync</c> commits successfully.
/// </summary>
/// <remarks>
/// CCA keeps this interface free of MediatR dependencies to preserve
/// the zero-dependency rule of <c>CCA.Core.Domain</c>.
/// The dispatcher in <c>CCA.Core.Persistence</c> wraps domain events
/// into MediatR notifications before dispatching.
/// </remarks>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the unique identifier of this domain event instance.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the UTC date and time when this event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}