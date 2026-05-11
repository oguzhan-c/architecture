namespace CCA.Core.Domain.Results;

/// <summary>
/// Represents a structured domain error with a code and a message.
/// Used as the failure value in <see cref="Result"/> and <see cref="Result{TValue}"/>.
/// </summary>
/// <remarks>
/// Error codes follow the convention <c>EntityName.ErrorType</c>,
/// for example <c>User.NotFound</c> or <c>Order.AlreadyShipped</c>.
/// Controllers use the code suffix to determine the HTTP status code automatically:
/// codes ending in <c>.NotFound</c> map to 404, <c>.Forbidden</c> to 403, etc.
/// </remarks>
/// <param name="Code">A unique machine-readable error identifier.</param>
/// <param name="Message">A human-readable description of the error.</param>
public record Error(string Code, string Message)
{
    /// <summary>
    /// Represents the absence of an error. Used as the error value for successful results.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Represents an error caused by a null value being provided where one is not allowed.
    /// </summary>
    public static readonly Error NullValue = new(
        "Error.NullValue",
        "A null value was provided.");

    /// <inheritdoc/>
    public override string ToString() => $"{Code}: {Message}";
}