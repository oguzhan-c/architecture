using Microsoft.EntityFrameworkCore;

namespace CCA.Core.Persistence.Specifications;

/// <summary>
/// Translates an <see cref="ISpecification{TEntity}"/> into an EF Core
/// <see cref="IQueryable{TEntity}"/> by applying criteria, includes,
/// ordering, and paging in sequence.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public static class SpecificationEvaluator<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Applies all components of the specification to the input queryable
    /// and returns the resulting queryable.
    /// </summary>
    /// <param name="inputQuery">The base EF Core queryable to apply the specification to.</param>
    /// <param name="specification">The specification to apply.</param>
    /// <returns>
    /// A new <see cref="IQueryable{TEntity}"/> with all specification
    /// components applied.
    /// </returns>
    public static IQueryable<TEntity> GetQuery(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification)
    {
        var query = inputQuery;

        // Apply all criteria (WHERE clauses)
        foreach (var criteria in specification.Criteria)
            query = query.Where(criteria);

        // Apply includes (JOINs / navigation properties)
        query = specification.Includes
            .Aggregate(query, (current, include) => include(current));

        // Apply ordering
        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        // Apply paging
        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip).Take(specification.Take);

        return query;
    }
}