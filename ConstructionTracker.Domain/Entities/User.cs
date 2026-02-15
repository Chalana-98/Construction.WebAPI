using ConstructionTracker.Domain.Common;

namespace ConstructionTracker.Domain.Entities;

/// <summary>
/// Represents a user within a tenant organization.
/// </summary>
public class User : BaseEntity, ITenantEntity, IAuditableEntity
{
    /// <summary>
    /// User's email address (used for login).
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Hashed password for authentication.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// User's phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// User's role within the organization (e.g., "Admin", "Manager", "Worker", "Viewer").
    /// </summary>
    public string Role { get; set; } = "Viewer";
    
    /// <summary>
    /// Job title of the user.
    /// </summary>
    public string? JobTitle { get; set; }
    
    /// <summary>
    /// Indicates whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Indicates whether the user's email has been verified.
    /// </summary>
    public bool EmailVerified { get; set; } = false;
    
    /// <summary>
    /// UTC timestamp of the user's last login.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
    
    /// <summary>
    /// Refresh token for JWT authentication.
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Expiration time of the refresh token.
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    /// <summary>
    /// Full name of the user.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
    
    /// <summary>
    /// Navigation property for the tenant this user belongs to.
    /// </summary>
    public virtual Tenant Tenant { get; set; } = null!;
}
