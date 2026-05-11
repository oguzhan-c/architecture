namespace CCA.Core.Persistence.Dynamic;

/// <summary>
/// Represents a single filter condition in a dynamic query.
/// Filters can be nested to create complex AND/OR expressions.
/// </summary>
public class Filter
{
    /// <summary>
    /// Gets or sets the field name to filter on.
    /// Supports nested navigation properties using dot notation (e.g. <c>"account.name"</c>).
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparison operator.
    /// Supported values: <c>eq</c>, <c>neq</c>, <c>contains</c>, <c>startswith</c>,
    /// <c>endswith</c>, <c>gt</c>, <c>gte</c>, <c>lt</c>, <c>lte</c>, <c>isnull</c>, <c>isnotnull</c>.
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// Gets or sets the value to compare against.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the logical operator used to combine this filter
    /// with sibling filters in <see cref="Filters"/>.
    /// Accepted values: <c>"and"</c>, <c>"or"</c>.
    /// </summary>
    public string? Logic { get; set; }

    /// <summary>
    /// Gets or sets nested child filters combined using <see cref="Logic"/>.
    /// Allows building complex multi-level filter expressions.
    /// </summary>
    public IEnumerable<Filter>? Filters { get; set; }
}