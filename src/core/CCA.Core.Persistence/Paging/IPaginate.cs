namespace CCA.Core.Persistence.Paging;

/// <summary>
/// Represents a paginated result set returned from a repository query.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public interface IPaginate<T>
{
    /// <summary>Gets the starting index offset. Always 0 for zero-based pagination.</summary>
    int From { get; }

    /// <summary>Gets the zero-based index of the current page.</summary>
    int Index { get; }

    /// <summary>Gets the number of items per page.</summary>
    int Size { get; }

    /// <summary>Gets the total number of records across all pages.</summary>
    int Count { get; }

    /// <summary>Gets the total number of pages.</summary>
    int Pages { get; }

    /// <summary>Gets the items on the current page.</summary>
    IList<T> Items { get; }

    /// <summary>Gets a value indicating whether a previous page exists.</summary>
    bool HasPrevious { get; }

    /// <summary>Gets a value indicating whether a next page exists.</summary>
    bool HasNext { get; }
}