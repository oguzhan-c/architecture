namespace CCA.Core.Domain.Events;

/// <summary>
/// Base record for all domain events.
/// Automatically assigns a new <see cref="IDomainEvent.Id"/>
/// and sets <see cref="IDomainEvent.OccurredOn"/> to the current UTC time.
/// </summary>
/// <example>
/// <code>
/// public record OrderCreatedEvent(Guid OrderId, Guid TenantId) : DomainEvent;
/// </code>
/// </example>
public abstract record DomainEvent : IDomainEvent
{
    /// <inheritdoc/>
    public Guid Id { get; } = Guid.NewGuid();

    /// <inheritdoc/>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}