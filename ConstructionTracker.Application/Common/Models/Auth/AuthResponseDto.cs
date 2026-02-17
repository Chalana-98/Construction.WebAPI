namespace ConstructionTracker.Application.Common.Models.Auth;

/// <summary>
/// DTO for authentication response containing JWT token and user information.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// JWT access token.
    /// </summary>
    public string Token { get; set; } = string.Empty;
    
    /// <summary>
    /// Token expiration date and time (UTC).
    /// </summary>
    public DateTime ExpiresAt { get; set; }
    
    /// <summary>
    /// User's unique identifier.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// User's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's role within the organization.
    /// </summary>
    public string Role { get; set; } = string.Empty;
    
    /// <summary>
    /// Tenant ID the user belongs to.
    /// </summary>
    public Guid TenantId { get; set; }
    
    /// <summary>
    /// Company name of the tenant.
    /// </summary>
    public string CompanyName { get; set; } = string.Empty;
}
