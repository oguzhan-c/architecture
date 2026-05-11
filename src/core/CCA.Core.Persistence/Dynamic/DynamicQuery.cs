namespace CCA.Core.Persistence.Dynamic;

/// <summary>
/// Represents a client-driven dynamic query containing
/// optional sort and filter definitions.
/// Sent by the client in the request body to filter and sort
/// data without requiring new endpoints.
/// </summary>
/// <example>
/// <code>
/// {
///   "sort": [{ "field": "createdDate", "dir": "desc" }],
///   "filter": {
///     "field": "isActive", "operator": "eq", "value": "true",
///     "logic": "and",
///     "filters": [
///       { "field": "name", "operator": "contains", "value": "pro" }
///     ]
///   }
/// }
/// </code>
/// </example>
public class DynamicQuery
{
    /// <summary>
    /// Gets or sets the list of sort conditions to apply.
    /// Multiple sorts are applied in order.
    /// </summary>
    public IEnumerable<Sort>? Sort { get; set; }

    /// <summary>
    /// Gets or sets the root filter condition.
    /// Nested filters can be defined via <see cref="Filter.Filters"/>.
    /// </summary>
    public Filter? Filter { get; set; }
}