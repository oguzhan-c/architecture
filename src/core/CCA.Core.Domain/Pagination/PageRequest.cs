namespace CCA.Core.Domain.Pagination;

/// <summary>
/// Represents pagination and sorting parameters sent by the client.
/// Passed to repository methods and query handlers to control
/// which page of data is returned.
/// </summary>
public class PageRequest
{
    /// <summary>
    /// Gets or sets the zero-based page index.
    /// Page 0 is the first page. Defaults to 0.
    /// </summary>
    public int PageIndex { get; set; } = 0;

    /// <summary>
    /// Gets or sets the number of items per page.
    /// Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the field name to sort by.
    /// <c>null</c> means no explicit sorting is applied.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sort direction is descending.
    /// <c>false</c> (ascending) by default.
    /// </summary>
    public bool SortDescending { get; set; }

    /// <summary>
    /// Gets the number of records to skip, computed from
    /// <see cref="PageIndex"/> and <see cref="PageSize"/>.
    /// Used directly in SQL <c>OFFSET</c> clauses.
    /// </summary>
    public int Skip => PageIndex * PageSize;
}