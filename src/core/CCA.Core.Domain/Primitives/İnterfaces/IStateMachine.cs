namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Defines a contract for entities that have a lifecycle status with
/// valid state transitions. Forces the entity to own its own state machine logic.
/// </summary>
/// <remarks>
/// Transition validation lives inside the entity — handlers never check
/// <c>if (entity.Status == X)</c> directly. They call <see cref="CanTransitionTo"/>
/// or a domain method that uses it internally.
/// </remarks>
/// <typeparam name="TStatus">The enum or type representing the entity status.</typeparam>
public interface IStateMachine<TStatus>
{
    /// <summary>
    /// Gets the current status of this entity.
    /// </summary>
    TStatus Status { get; }

    /// <summary>
    /// Determines whether a transition to the specified status is valid
    /// from the current status.
    /// </summary>
    /// <param name="newStatus">The target status to transition to.</param>
    /// <returns>
    /// <c>true</c> if the transition is valid; otherwise <c>false</c>.
    /// </returns>
    bool CanTransitionTo(TStatus newStatus);
}