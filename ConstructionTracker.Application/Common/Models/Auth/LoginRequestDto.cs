using System.ComponentModel.DataAnnotations;

namespace ConstructionTracker.Application.Common.Models.Auth;

/// <summary>
/// DTO for user login request.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// User's email address.
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
