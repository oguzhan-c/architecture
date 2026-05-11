namespace CCA.Core.Persistence.Dynamic;

/// <summary>
/// Represents a single sort condition in a dynamic query.
/// </summary>
public class Sort
{
    /// <summary>
    /// Gets or sets the field name to sort by.
    /// Supports nested navigation properties using dot notation.
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort direction.
    /// Accepted values: <c>"asc"</c> (ascending), <c>"desc"</c> (descending).
    /// </summary>
    public string Dir { get; set; } = "asc";
}