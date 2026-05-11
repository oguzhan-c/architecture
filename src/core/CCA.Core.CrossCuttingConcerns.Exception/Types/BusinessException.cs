namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when a business rule or domain invariant is violated inside
/// infrastructure code where <c>Result&lt;T&gt;</c> cannot be returned
/// (e.g. EF Core interceptors, middleware).
/// </summary>
/// <remarks>
/// For business failures inside handlers and business rules,
/// use <c>Result.Failure(Error)</c> instead of throwing this exception.
/// This exception is reserved for structural violations only.
/// </remarks>
public class BusinessException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="BusinessException"/>
    /// with the specified error message.
    /// </summary>
    /// <param name="message">The message describing the business rule violation.</param>
    public BusinessException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="BusinessException"/>
    /// with a message and an inner exception.
    /// </summary>
    /// <param name="message">The message describing the business rule violation.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public BusinessException(string message, System.Exception innerException)
        : base(message, innerException) { }
}