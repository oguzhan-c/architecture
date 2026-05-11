using System.Linq.Dynamic.Core;
using System.Text;

namespace CCA.Core.Persistence.Dynamic;

/// <summary>
/// Provides extension methods for applying <see cref="DynamicQuery"/>
/// filter and sort expressions to an <see cref="IQueryable{T}"/>.
/// Uses <c>System.Linq.Dynamic.Core</c> to translate dynamic queries into SQL.
/// </summary>
public static class IQueryableDynamicFilterExtensions
{
    private static readonly string[] _orders = ["asc", "desc"];
    private static readonly string[] _logics = ["and", "or"];

    private static readonly IDictionary<string, string> _operators =
        new Dictionary<string, string>
        {
            { "eq",           "=" },
            { "neq",          "!=" },
            { "lt",           "<" },
            { "lte",          "<=" },
            { "gt",           ">" },
            { "gte",          ">=" },
            { "isnull",       "== null" },
            { "isnotnull",    "!= null" },
            { "startswith",   "StartsWith" },
            { "endswith",     "EndsWith" },
            { "contains",     "Contains" },
            { "doesnotcontain", "Contains" },
        };

    /// <summary>
    /// Applies the <see cref="DynamicQuery"/> sort and filter expressions
    /// to the specified <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="queryable">The source queryable to apply the dynamic query to.</param>
    /// <param name="dynamicQuery">The dynamic query containing sort and filter definitions.</param>
    /// <returns>A new <see cref="IQueryable{T}"/> with the dynamic query applied.</returns>
    public static IQueryable<T> ToDynamic<T>(
        this IQueryable<T> queryable,
        DynamicQuery dynamicQuery)
    {
        if (dynamicQuery.Filter is not null)
            queryable = Filter(queryable, dynamicQuery.Filter);

        if (dynamicQuery.Sort is not null && dynamicQuery.Sort.Any())
            queryable = Sort(queryable, dynamicQuery.Sort);

        return queryable;
    }

    private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
    {
        var filters = GetAllFilters(filter);
        var values = filters
            .Select(f => f.Value)
            .ToArray();

        string where = Transform(filter, filters);

        if (!string.IsNullOrEmpty(where) && values.Length > 0)
            queryable = queryable.Where(where, values);

        return queryable;
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sorts)
    {
        var ordering = string.Join(
            ",",
            sorts.Select(s =>
            {
                var dir = _orders.Contains(s.Dir.ToLower())
                    ? s.Dir
                    : "asc";
                return $"{s.Field} {dir}";
            }));

        return queryable.OrderBy(ordering);
    }

    private static IList<Filter> GetAllFilters(Filter filter)
    {
        var filters = new List<Filter>();
        GetFilters(filter, filters);
        return filters;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters is not null && filter.Filters.Any())
            foreach (var child in filter.Filters)
                GetFilters(child, filters);
    }

    private static string Transform(Filter filter, IList<Filter> filters)
    {
        var index = filters.IndexOf(filter);
        var left = filter.Field;
        var op = filter.Operator?.ToLower() ?? "eq";

        if (!_operators.TryGetValue(op, out var comparison))
            comparison = "=";

        string right;

        if (op is "isnull" or "isnotnull")
            right = comparison;
        else if (op is "startswith" or "endswith" or "contains")
            right = $".{comparison}(@{index})";
        else if (op == "doesnotcontain")
            right = $".Contains(@{index}) == false";
        else
            right = $"{comparison} @{index}";

        if (filter.Filters is not null && filter.Filters.Any())
        {
            var logic = _logics.Contains(filter.Logic?.ToLower())
                ? filter.Logic!.ToUpper()
                : "AND";

            var children = filter.Filters
                .Select(child => Transform(child, filters));

            var sb = new StringBuilder();
            sb.Append('(');

            if (op is not "isnull" and not "isnotnull")
                sb.Append($"{left} {right}");

            foreach (var child in children)
                sb.Append($" {logic} {child}");

            sb.Append(')');
            return sb.ToString();
        }

        if (op is "isnull" or "isnotnull")
            return $"{left} {right}";

        if (op is "startswith" or "endswith" or "contains")
            return $"{left}{right}";

        if (op == "doesnotcontain")
            return $"!({left}{right})";

        return $"{left} {right}";
    }
}