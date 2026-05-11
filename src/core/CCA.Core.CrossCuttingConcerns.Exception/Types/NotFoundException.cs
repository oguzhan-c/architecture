namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when a required resource cannot be found.
/// Maps to HTTP 404 Not Found.
/// </summary>
/// <remarks>
/// Prefer returning <c>Result.Failure(Error)</c> with an error code
/// ending in <c>.NotFound</c> from handlers.
/// Use this exception only in infrastructure code where
/// <c>Result&lt;T&gt;</c> cannot be returned.
/// </remarks>
public class NotFoundException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundException"/>
    /// with the specified message.
    /// </summary>
    /// <param name="message">The message describing what was not found.</param>
    public NotFoundException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundException"/>
    /// with a message and inner exception.
    /// </summary>
    /// <param name="message">The message describing what was not found.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public NotFoundException(string message, System.Exception innerException)
        : base(message, innerException) { }
}