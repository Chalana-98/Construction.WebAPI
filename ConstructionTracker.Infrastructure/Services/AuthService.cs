using ConstructionTracker.Application.Common.Interfaces;
using ConstructionTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConstructionTracker.Infrastructure.Services;

/// <summary>
/// Service for authentication operations including registration and login.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterCompanyAsync(
        string companyName,
        string subdomain,
        string firstName,
        string lastName,
        string email,
        string password,
        string? contactPhone,
        CancellationToken cancellationToken = default)
    {
        // Check if email already exists (ignore tenant filter for global uniqueness check)
        var existingUser = await _context.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
        
        if (existingUser != null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        // Check if subdomain already exists
        var existingTenant = await _context.Tenants
            .FirstOrDefaultAsync(t => t.Subdomain.ToLower() == subdomain.ToLower(), cancellationToken);
        
        if (existingTenant != null)
        {
            throw new InvalidOperationException("This subdomain is already taken.");
        }

        // Create new tenant
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            CompanyName = companyName,
            Subdomain = subdomain.ToLower(),
            ContactEmail = email,
            ContactPhone = contactPhone,
            SubscriptionPlan = "free",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Tenants.Add(tenant);

        // Create admin user
        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email.ToLower(),
            Role = "Admin",
            IsActive = true,
            EmailVerified = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Hash password using ASP.NET Identity PasswordHasher
        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);

        // Save changes
        await _context.SaveChangesAsync(cancellationToken);

        // Load tenant navigation property
        user.Tenant = tenant;

        return user;
    }

    public async Task<User?> AuthenticateAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        // Find user by email (ignore tenant filter - auth is cross-tenant)
        var user = await _context.Users
            .IgnoreQueryFilters()
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);

        if (user == null)
        {
            return null;
        }

        // Check if user is active
        if (!user.IsActive)
        {
            throw new InvalidOperationException("User account is inactive.");
        }

        // Check if tenant is active
        if (!user.Tenant.IsActive)
        {
            throw new InvalidOperationException("Company account is inactive.");
        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        // Update last login timestamp
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}
