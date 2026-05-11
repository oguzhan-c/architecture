namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when input validation fails.
/// Carries a list of field-level validation errors.
/// Maps to HTTP 422 Unprocessable Entity.
/// </summary>
/// <remarks>
/// Raised automatically by the FluentValidation pipeline behavior
/// in <c>CCA.Core.Application</c> before the handler runs.
/// </remarks>
public class ValidationException : System.Exception
{
    /// <summary>
    /// Gets the list of validation errors describing which fields failed and why.
    /// </summary>
    public IReadOnlyList<ValidationError> Errors { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="ValidationException"/>
    /// with a list of validation errors.
    /// </summary>
    /// <param name="errors">The validation errors that caused this exception.</param>
    public ValidationException(IEnumerable<ValidationError> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToList().AsReadOnly();
    }
}

/// <summary>
/// Represents a single field-level validation error.
/// </summary>
/// <param name="Field">The name of the field that failed validation.</param>
/// <param name="Message">The validation error message for this field.</param>
public record ValidationError(string Field, string Message);