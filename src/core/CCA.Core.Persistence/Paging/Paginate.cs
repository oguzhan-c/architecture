namespace CCA.Core.Persistence.Paging;

/// <summary>
/// Default implementation of <see cref="IPaginate{T}"/>.
/// Created by <see cref="QueryablePaginateExtensions.ToPaginateAsync{T}"/>.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public class Paginate<T> : IPaginate<T>
{
    /// <summary>
    /// Initializes a new instance of <see cref="Paginate{T}"/> from a source list.
    /// </summary>
    /// <param name="source">The full list of items to paginate.</param>
    /// <param name="index">The zero-based page index.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="from">The starting offset. Defaults to 0.</param>
    public Paginate(IEnumerable<T> source, int index, int size, int from = 0)
    {
        From = from;
        Index = index;
        Size = size;

        var enumerable = source.ToList();
        Count = enumerable.Count;
        Pages = Size > 0 ? (int)Math.Ceiling(Count / (double)Size) : 0;
        Items = enumerable
            .Skip((Index - From) * Size)
            .Take(Size)
            .ToList();
    }

    /// <summary>
    /// Initializes an empty <see cref="Paginate{T}"/>.
    /// </summary>
    internal Paginate()
    {
        Items = new List<T>();
    }

    /// <inheritdoc/>
    public int From { get; set; }

    /// <inheritdoc/>
    public int Index { get; set; }

    /// <inheritdoc/>
    public int Size { get; set; }

    /// <inheritdoc/>
    public int Count { get; set; }

    /// <inheritdoc/>
    public int Pages { get; set; }

    /// <inheritdoc/>
    public IList<T> Items { get; set; }

    /// <inheritdoc/>
    public bool HasPrevious => Index - From > 0;

    /// <inheritdoc/>
    public bool HasNext => Index - From + 1 < Pages;
}