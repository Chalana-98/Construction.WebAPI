namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Interface for accessing the current tenant context.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Gets the current tenant ID from the request context.
    /// </summary>
    Guid GetCurrentTenantId();
    
    /// <summary>
    /// Gets the current user ID from the request context.
    /// </summary>
    Guid GetCurrentUserId();
    
    /// <summary>
    /// Sets the current tenant ID (for testing or background jobs).
    /// </summary>
    void SetCurrentTenantId(Guid tenantId);
    
    /// <summary>
    /// Sets the current user ID (for testing or background jobs).
    /// </summary>
    void SetCurrentUserId(Guid userId);
}
