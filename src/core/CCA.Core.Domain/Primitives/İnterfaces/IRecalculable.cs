namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Defines a contract for entities that have computed fields derived
/// from their child collections or other properties.
/// Forces the entity to own its own recalculation logic.
/// </summary>
/// <remarks>
/// Entities implementing this interface call <see cref="Recalculate"/>
/// inside every mutating method that affects the computed fields.
/// Handlers never calculate totals, aggregates, or derived values directly.
/// This pattern was adopted from the EFisher production project.
/// </remarks>
public interface IRecalculable
{
    /// <summary>
    /// Recomputes all derived fields from the current state of the entity.
    /// Called internally after any mutation that affects computed values.
    /// </summary>
    void Recalculate();
}