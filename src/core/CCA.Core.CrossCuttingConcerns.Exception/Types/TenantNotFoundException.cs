namespace CCA.Core.CrossCuttingConcerns.Exception.Types;

/// <summary>
/// Thrown when the current tenant cannot be resolved from the HTTP request.
/// Maps to HTTP 400 Bad Request.
/// </summary>
/// <remarks>
/// Raised by the tenant resolution middleware when no valid tenant identifier
/// is found in the request header, subdomain, or JWT claim.
/// </remarks>
public class TenantNotFoundException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="TenantNotFoundException"/>
    /// with a default message.
    /// </summary>
    public TenantNotFoundException()
        : base("Tenant could not be resolved from the request.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="TenantNotFoundException"/>
    /// with the specified message.
    /// </summary>
    /// <param name="message">The message describing why the tenant was not found.</param>
    public TenantNotFoundException(string message) : base(message) { }
}