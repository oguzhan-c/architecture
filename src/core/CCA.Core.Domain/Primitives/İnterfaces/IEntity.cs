namespace CCA.Core.Domain.Primitives;

/// <summary>
/// Defines the base contract for all entities in the domain.
/// Every entity has a strongly-typed identifier.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets the unique identifier of this entity.
    /// </summary>
    TId Id { get; }
}