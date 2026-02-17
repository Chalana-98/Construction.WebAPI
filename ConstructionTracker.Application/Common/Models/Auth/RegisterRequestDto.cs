using System.ComponentModel.DataAnnotations;

namespace ConstructionTracker.Application.Common.Models.Auth;

/// <summary>
/// DTO for company registration request.
/// </summary>
public class RegisterRequestDto
{
    /// <summary>
    /// Company name for the new tenant.
    /// </summary>
    [Required(ErrorMessage = "Company name is required")]
    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string CompanyName { get; set; } = string.Empty;
    
    /// <summary>
    /// Subdomain for the tenant (e.g., "acme" for acme.constructiontracker.com).
    /// </summary>
    [Required(ErrorMessage = "Subdomain is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Subdomain must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Subdomain can only contain lowercase letters, numbers, and hyphens")]
    public string Subdomain { get; set; } = string.Empty;
    
    /// <summary>
    /// Admin user's first name.
    /// </summary>
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Admin user's last name.
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Admin user's email address (used for login).
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Admin user's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Contact phone number for the tenant.
    /// </summary>
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? ContactPhone { get; set; }
}
