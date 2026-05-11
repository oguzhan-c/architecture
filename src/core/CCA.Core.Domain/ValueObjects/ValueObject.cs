namespace CCA.Core.Domain.ValueObjects;

/// <summary>
/// Base class for DDD value objects.
/// Value objects have no identity — two instances with the same
/// property values are considered equal.
/// </summary>
/// <remarks>
/// Implement <see cref="GetEqualityComponents"/> to define which
/// properties participate in equality comparison.
/// Value objects should be immutable — all properties set in the constructor only.
/// </remarks>
/// <example>
/// <code>
/// public class Address : ValueObject
/// {
///     public string City { get; }
///     public string PostalCode { get; }
///
///     public Address(string city, string postalCode)
///     {
///         City = city;
///         PostalCode = postalCode;
///     }
///
///     protected override IEnumerable&lt;object?&gt; GetEqualityComponents()
///     {
///         yield return City;
///         yield return PostalCode;
///     }
/// }
/// </code>
/// </example>
public abstract class ValueObject
{
    /// <summary>
    /// Returns the components used to determine equality for this value object.
    /// Each yielded value participates in the equality and hash code calculation.
    /// </summary>
    /// <returns>The equality components of this value object.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc/>
    public override int GetHashCode() =>
        GetEqualityComponents()
            .Aggregate(default(int), HashCode.Combine);

    /// <summary>
    /// Determines whether two value objects are equal by value.
    /// </summary>
    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    /// <summary>
    /// Determines whether two value objects are not equal by value.
    /// </summary>
    public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);
}