namespace ConstructionTracker.Domain.Enums;

/// <summary>
/// User roles within the application.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// View-only access to data.
    /// </summary>
    Viewer = 0,
    
    /// <summary>
    /// Worker with basic data entry capabilities.
    /// </summary>
    Worker = 1,
    
    /// <summary>
    /// Manager with project management capabilities.
    /// </summary>
    Manager = 2,
    
    /// <summary>
    /// Administrator with full access to tenant data.
    /// </summary>
    Admin = 3,
    
    /// <summary>
    /// Super administrator with cross-tenant access (platform owner).
    /// </summary>
    SuperAdmin = 99
}
