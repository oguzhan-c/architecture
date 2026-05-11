namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when an operation cannot be completed because it conflicts
/// with the current state of a resource.
/// Maps to HTTP 409 Conflict.
/// </summary>
/// <remarks>
/// Examples: duplicate unique key, optimistic concurrency conflict,
/// attempting to create a resource that already exists.
/// </remarks>
public class ConflictException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConflictException"/>
    /// with the specified message.
    /// </summary>
    /// <param name="message">The message describing the conflict.</param>
    public ConflictException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ConflictException"/>
    /// with a message and inner exception.
    /// </summary>
    /// <param name="message">The message describing the conflict.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public ConflictException(string message, System.Exception innerException)
        : base(message, innerException) { }
}