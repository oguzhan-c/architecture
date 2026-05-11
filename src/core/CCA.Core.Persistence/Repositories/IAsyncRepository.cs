using System.Linq.Expressions;
using CCA.Core.Domain.Pagination;
using CCA.Core.Domain.Primitives.Interfaces;
using CCA.Core.Persistence.Dynamic;
using CCA.Core.Persistence.Paging;
using CCA.Core.Persistence.Specifications;
using Microsoft.EntityFrameworkCore.Query;

namespace CCA.Core.Persistence.Repositories;

/// <summary>
/// Defines asynchronous CRUD operations for a domain entity.
/// Supports dynamic querying, pagination, specification pattern,
/// soft delete, and multi-tenant filtering.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IAsyncRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    /// <summary>
    /// Asynchronously gets a single entity by its identifier.
    /// Returns <c>null</c> if no entity with the specified Id exists.
    /// </summary>
    /// <param name="id">The entity identifier to search for.</param>
    /// <param name="include">Optional navigation properties to include.</param>
    /// <param name="withDeleted">
    /// <c>true</c> to include soft-deleted records; <c>false</c> to exclude them.
    /// </param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The entity if found; otherwise <c>null</c>.</returns>
    Task<TEntity?> GetByIdAsync(
        TId id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single entity matching the specified predicate.
    /// Returns <c>null</c> if no match is found.
    /// </summary>
    /// <param name="predicate">The filter expression.</param>
    /// <param name="include">Optional navigation properties to include.</param>
    /// <param name="withDeleted">
    /// <c>true</c> to include soft-deleted records; <c>false</c> to exclude them.
    /// </param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The entity if found; otherwise <c>null</c>.</returns>
    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single entity matching the specified specification.
    /// Returns <c>null</c> if no match is found.
    /// </summary>
    /// <param name="specification">The specification to apply.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The entity if found; otherwise <c>null</c>.</returns>
    Task<TEntity?> GetAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The optional filter expression.</param>
    /// <param name="orderBy">The optional ordering expression.</param>
    /// <param name="include">Optional navigation properties to include.</param>
    /// <param name="pageRequest">The pagination parameters.</param>
    /// <param name="withDeleted">
    /// <c>true</c> to include soft-deleted records; <c>false</c> to exclude them.
    /// </param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated result containing the requested page of items.</returns>
    Task<IPaginate<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        PageRequest? pageRequest = null,
        bool withDeleted = false,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of entities matching the specified specification.
    /// </summary>
    /// <param name="specification">The specification to apply.</param>
    /// <param name="pageRequest">The pagination parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated result containing the requested page of items.</returns>
    Task<IPaginate<TEntity>> GetListAsync(
        ISpecification<TEntity> specification,
        PageRequest? pageRequest = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of entities using a dynamic query for
    /// client-driven filtering and sorting.
    /// </summary>
    /// <param name="dynamic">The dynamic query containing filter and sort definitions.</param>
    /// <param name="include">Optional navigation properties to include.</param>
    /// <param name="pageRequest">The pagination parameters.</param>
    /// <param name="withDeleted">
    /// <c>true</c> to include soft-deleted records; <c>false</c> to exclude them.
    /// </param>
    /// <param name="enableTracking">
    /// <c>true</c> to enable EF Core change tracking; <c>false</c> for read-only queries.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated result containing the requested page of items.</returns>
    Task<IPaginate<TEntity>> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        PageRequest? pageRequest = null,
        bool withDeleted = false,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The optional filter expression.</param>
    /// <param name="withDeleted">
    /// <c>true</c> to include soft-deleted records in the check.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> if a matching entity exists; otherwise <c>false</c>.</returns>
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>Adds a new entity to the data store.</summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<TEntity> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>Adds a collection of entities to the data store.</summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    /// <summary>Updates an existing entity in the data store.</summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated entity.</returns>
    Task<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a collection of entities in the data store.</summary>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the data store.
    /// If the entity implements <c>ISoftDeletable</c>, performs a soft delete
    /// by setting <c>DeletedAt</c> instead of physically removing the record.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deleted entity.</returns>
    Task<TEntity> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a collection of entities.
    /// Applies soft delete if entities implement <c>ISoftDeletable</c>.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
}