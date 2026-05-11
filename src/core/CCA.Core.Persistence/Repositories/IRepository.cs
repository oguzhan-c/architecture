using System.Linq.Expressions;

namespace CCA.Core.Persistence.Repositories;

/// <summary>
/// Defines synchronous CRUD operations for a domain entity.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : class
{
    /// <summary>
    /// Gets a single entity matching the specified predicate.
    /// Returns <c>null</c> if no match is found.
    /// </summary>
    /// <param name="predicate">The filter expression.</param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        bool enableTracking = false);

    /// <summary>
    /// Gets a single entity by its identifier.
    /// Returns <c>null</c> if no entity with the specified Id exists.
    /// </summary>
    /// <param name="id">The entity identifier to search for.</param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    TEntity? GetById(TId id, bool enableTracking = false);
    
    /// <summary>
    /// Gets all entities matching the specified predicate.
    /// Returns an empty list if no matches are found.
    /// </summary>
    /// <param name="predicate">The optional filter expression.</param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    IList<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = false);

    /// <summary>Adds a new entity to the data store.</summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    TEntity Add(TEntity entity);

    /// <summary>Updates an existing entity in the data store.</summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    TEntity Update(TEntity entity);

    /// <summary>Removes an entity from the data store.</summary>
    /// <param name="entity">The entity to remove.</param>
    TEntity Delete(TEntity entity);
}

