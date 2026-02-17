using ConstructionTracker.Domain.Entities;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Interface for authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new company (tenant) with an admin user.
    /// </summary>
    /// <param name="companyName">Company name.</param>
    /// <param name="subdomain">Subdomain for the tenant.</param>
    /// <param name="firstName">Admin user's first name.</param>
    /// <param name="lastName">Admin user's last name.</param>
    /// <param name="email">Admin user's email.</param>
    /// <param name="password">Admin user's password.</param>
    /// <param name="contactPhone">Optional contact phone.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created user with tenant information.</returns>
    Task<User> RegisterCompanyAsync(
        string companyName,
        string subdomain,
        string firstName,
        string lastName,
        string email,
        string password,
        string? contactPhone,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Authenticated user if credentials are valid, null otherwise.</returns>
    Task<User?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
}
