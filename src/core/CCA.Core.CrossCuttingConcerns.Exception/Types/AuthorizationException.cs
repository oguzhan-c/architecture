namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when an authenticated user attempts an action
/// they do not have permission to perform.
/// Maps to HTTP 403 Forbidden.
/// </summary>
public class AuthorizationException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="AuthorizationException"/>
    /// with a default message.
    /// </summary>
    public AuthorizationException()
        : base("You do not have permission to perform this action.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="AuthorizationException"/>
    /// with the specified message.
    /// </summary>
    /// <param name="message">The message describing the authorization failure.</param>
    public AuthorizationException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="AuthorizationException"/>
    /// with a message and an inner exception.
    /// </summary>
    /// <param name="message">The message describing the authorization failure.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public AuthorizationException(string message, System.Exception innerException)
        : base(message, innerException) { }
}