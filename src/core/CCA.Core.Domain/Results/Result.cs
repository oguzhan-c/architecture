namespace CCA.Core.Domain.Results;

/// <summary>
/// Represents the outcome of an operation that can either succeed or fail.
/// Used instead of throwing exceptions for expected business failures.
/// </summary>
/// <remarks>
/// Always use <see cref="Success()"/> or <see cref="Failure(Error)"/> factory methods
/// to create instances. Direct construction is not allowed.
/// <para>
/// For operations that return a value on success, use <see cref="Result{TValue}"/>.
/// </para>
/// </remarks>
public class Result
{
    /// <summary>
    /// Initializes a new instance of <see cref="Result"/>.
    /// </summary>
    /// <param name="isSuccess">Whether the operation succeeded.</param>
    /// <param name="error">The error if the operation failed.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a success result has a non-empty error,
    /// or if a failure result has an empty error.
    /// </exception>
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException(
                "A successful result cannot carry an error.");
        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException(
                "A failed result must carry an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error associated with a failed result.
    /// Returns <see cref="Error.None"/> for successful results.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful result with no value.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a successful result carrying a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <param name="value">The value to carry on success.</param>
    /// <returns>A successful <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    /// <summary>
    /// Creates a failed result of the specified value type.
    /// </summary>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);
}