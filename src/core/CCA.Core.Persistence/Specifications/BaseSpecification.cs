using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace CCA.Core.Persistence.Specifications;

/// <summary>
/// Base class for building specifications using a fluent API.
/// Extend this class to create named, reusable query objects.
/// </summary>
/// <example>
/// <code>
/// public class ActiveProductsByTenantSpec : BaseSpecification&lt;Product&gt;
/// {
///     public ActiveProductsByTenantSpec(Guid tenantId, string? search, PageRequest page)
///     {
///         AddCriteria(p => p.IsActive &amp;&amp; p.TenantId == tenantId);
///         if (!string.IsNullOrEmpty(search))
///             AddCriteria(p => p.Name.Contains(search));
///         AddInclude(p => p.Category);
///         ApplyOrderByDescending(p => p.CreatedDate);
///         ApplyPaging(page.Skip, page.PageSize);
///     }
/// }
/// </code>
/// </example>
/// <typeparam name="TEntity">The entity type this specification applies to.</typeparam>
public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    /// <inheritdoc/>
    public IList<Expression<Func<TEntity, bool>>> Criteria { get; }
        = new List<Expression<Func<TEntity, bool>>>();

    /// <inheritdoc/>
    public IList<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }
        = new List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    /// <inheritdoc/>
    public bool IsPagingEnabled { get; private set; }

    /// <inheritdoc/>
    public int Skip { get; private set; }

    /// <inheritdoc/>
    public int Take { get; private set; }

    /// <summary>
    /// Adds a filter expression to the specification.
    /// Multiple criteria are combined using AND logic.
    /// </summary>
    /// <param name="criteria">The filter expression to add.</param>
    protected void AddCriteria(Expression<Func<TEntity, bool>> criteria) =>
        Criteria.Add(criteria);

    /// <summary>
    /// Adds a navigation property include expression to the specification.
    /// </summary>
    /// <param name="include">The include expression to add.</param>
    protected void AddInclude(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) =>
        Includes.Add(include);

    /// <summary>
    /// Sets the ascending order expression for this specification.
    /// </summary>
    /// <param name="orderBy">The property to order by ascending.</param>
    protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderBy) =>
        OrderBy = orderBy;

    /// <summary>
    /// Sets the descending order expression for this specification.
    /// </summary>
    /// <param name="orderByDescending">The property to order by descending.</param>
    protected void ApplyOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescending) =>
        OrderByDescending = orderByDescending;

    /// <summary>
    /// Enables paging on this specification with the specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of records to skip.</param>
    /// <param name="take">The maximum number of records to return.</param>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}