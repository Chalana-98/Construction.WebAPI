using ConstructionTracker.Domain.Entities;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Interface for JWT token generation and management.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user to generate token for.</param>
    /// <returns>JWT token string.</returns>
    string GenerateToken(User user);
    
    /// <summary>
    /// Validates a JWT token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>True if token is valid, false otherwise.</returns>
    bool ValidateToken(string token);
    
    /// <summary>
    /// Gets the user ID from a JWT token.
    /// </summary>
    /// <param name="token">The token to extract user ID from.</param>
    /// <returns>User ID if found, null otherwise.</returns>
    Guid? GetUserIdFromToken(string token);
}
