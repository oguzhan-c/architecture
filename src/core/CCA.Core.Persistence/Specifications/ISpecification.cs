using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace CCA.Core.Persistence.Specifications;

/// <summary>
/// Defines a reusable, named query specification that encapsulates
/// filter criteria, ordering, includes, and pagination logic.
/// </summary>
/// <remarks>
/// The Specification pattern eliminates duplicated query logic across handlers.
/// Define a specification once and reuse it everywhere the same query is needed.
/// The <see cref="SpecificationEvaluator{TEntity}"/> translates this into
/// an EF Core <see cref="IQueryable{T}"/>.
/// </remarks>
/// <typeparam name="TEntity">The entity type this specification applies to.</typeparam>
public interface ISpecification<TEntity>
{
    /// <summary>
    /// Gets the list of filter expressions applied as WHERE clauses.
    /// All criteria are combined with AND logic.
    /// </summary>
    IList<Expression<Func<TEntity, bool>>> Criteria { get; }

    /// <summary>
    /// Gets the list of navigation property include expressions.
    /// </summary>
    IList<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }

    /// <summary>
    /// Gets the optional ascending order expression.
    /// </summary>
    Expression<Func<TEntity, object>>? OrderBy { get; }

    /// <summary>
    /// Gets the optional descending order expression.
    /// </summary>
    Expression<Func<TEntity, object>>? OrderByDescending { get; }

    /// <summary>
    /// Gets a value indicating whether paging has been configured
    /// on this specification.
    /// </summary>
    bool IsPagingEnabled { get; }

    /// <summary>
    /// Gets the number of records to skip.
    /// Only used when <see cref="IsPagingEnabled"/> is <c>true</c>.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets the maximum number of records to return.
    /// Only used when <see cref="IsPagingEnabled"/> is <c>true</c>.
    /// </summary>
    int Take { get; }
}