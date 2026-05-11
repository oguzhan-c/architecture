namespace CCA.Core.Domain.Pagination;

/// <summary>
/// Represents a paginated response returned to the client.
/// Contains the current page of items and metadata about the full result set.
/// </summary>
/// <typeparam name="T">The type of items in this page.</typeparam>
public class PagedResponse<T>
{
    /// <summary>
    /// Gets or sets the items on the current page.
    /// </summary>
    public IList<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Gets or sets the zero-based index of the current page.
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of records across all pages.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets the total number of pages based on
    /// <see cref="TotalCount"/> and <see cref="PageSize"/>.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Gets a value indicating whether a previous page exists.
    /// </summary>
    public bool HasPrevious => PageIndex > 0;

    /// <summary>
    /// Gets a value indicating whether a next page exists.
    /// </summary>
    public bool HasNext => PageIndex < TotalPages - 1;

    /// <summary>
    /// Creates a <see cref="PagedResponse{T}"/> from a list of items,
    /// a total count, and the original page request.
    /// </summary>
    /// <param name="items">The items on the current page.</param>
    /// <param name="totalCount">The total number of records across all pages.</param>
    /// <param name="request">The original page request.</param>
    /// <returns>A populated <see cref="PagedResponse{T}"/>.</returns>
    public static PagedResponse<T> Create(
        IList<T> items,
        int totalCount,
        PageRequest request) => new()
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
}