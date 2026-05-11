using Microsoft.EntityFrameworkCore;

namespace CCA.Core.Persistence.Paging;

/// <summary>
/// Provides extension methods for applying pagination to <see cref="IQueryable{T}"/>.
/// </summary>
public static class QueryablePaginateExtensions
{
    /// <summary>
    /// Asynchronously applies pagination to the specified queryable
    /// and returns an <see cref="IPaginate{T}"/>.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="source">The source queryable to paginate.</param>
    /// <param name="index">The zero-based page index.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="from">The starting offset. Defaults to 0.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated result containing the requested page of items.</returns>
    public static async Task<IPaginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        int from = 0,
        CancellationToken cancellationToken = default)
    {
        if (from > index)
            throw new ArgumentException(
                $"{nameof(from)} must be less than or equal to {nameof(index)}.",
                nameof(from));

        var count = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((index - from) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new Paginate<T>
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = items,
            Pages = size > 0 ? (int)Math.Ceiling(count / (double)size) : 0
        };
    }
}