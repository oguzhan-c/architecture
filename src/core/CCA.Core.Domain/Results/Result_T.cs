namespace CCA.Core.Domain.Results;

/// <summary>
/// Represents the outcome of an operation that returns a value on success.
/// On success, <see cref="Value"/> contains the result.
/// On failure, <see cref="Result.Error"/> describes what went wrong.
/// </summary>
/// <typeparam name="TValue">The type of the value returned on success.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a new instance of <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <param name="isSuccess">Whether the operation succeeded.</param>
    /// <param name="error">The error if the operation failed.</param>
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value of a successful result.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when accessed on a failed result.
    /// Always check <see cref="Result.IsSuccess"/> before accessing this property.
    /// </exception>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException(
            $"Cannot access the Value of a failed result. Error: {Error}");

    /// <summary>
    /// Implicitly converts a value to a successful <see cref="Result{TValue}"/>.
    /// Allows returning a value directly from methods that return <c>Result&lt;T&gt;</c>.
    /// </summary>
    /// <param name="value">The value to wrap in a successful result.</param>
    public static implicit operator Result<TValue>(TValue value) =>
        Result.Success(value);
}