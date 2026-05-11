namespace CCA.Core.Domain.Primitives.Classes;

/// <summary>
/// Provides static guard methods for validating method arguments.
/// Replaces external libraries (e.g. Ardalis.GuardClauses) to keep
/// <c>CCA.Core.Domain</c> free of NuGet dependencies.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the value is null or whitespace.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is null, empty, or whitespace.
    /// </exception>
    public static string NullOrWhiteSpace(
        string? value,
        string paramName,
        string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                message ?? $"{paramName} cannot be null or empty.",
                paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="value"/> is null.
    /// </exception>
    public static T Null<T>(
        T? value,
        string paramName,
        string? message = null) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(
                paramName,
                message ?? $"{paramName} cannot be null.");
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the decimal value is negative or zero.
    /// </summary>
    /// <param name="value">The decimal value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is less than or equal to zero.
    /// </exception>
    public static decimal NegativeOrZero(
        decimal value,
        string paramName,
        string? message = null)
    {
        if (value <= 0)
            throw new ArgumentException(
                message ?? $"{paramName} must be greater than zero.",
                paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the integer value is negative or zero.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is less than or equal to zero.
    /// </exception>
    public static int NegativeOrZero(
        int value,
        string paramName,
        string? message = null)
    {
        if (value <= 0)
            throw new ArgumentException(
                message ?? $"{paramName} must be greater than zero.",
                paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the Guid value is empty.
    /// </summary>
    /// <param name="value">The Guid value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> equals <see cref="Guid.Empty"/>.
    /// </exception>
    public static Guid EmptyGuid(
        Guid value,
        string paramName,
        string? message = null)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                message ?? $"{paramName} cannot be an empty Guid.",
                paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is outside
    /// the specified inclusive range.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="value"/> is less than <paramref name="min"/>
    /// or greater than <paramref name="max"/>.
    /// </exception>
    public static int OutOfRange(
        int value,
        string paramName,
        int min,
        int max,
        string? message = null)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(
                paramName,
                message ?? $"{paramName} must be between {min} and {max}.");
        return value;
    }
}