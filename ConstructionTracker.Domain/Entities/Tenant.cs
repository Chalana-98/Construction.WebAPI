namespace ConstructionTracker.Domain.Entities;

/// <summary>
/// Represents a tenant (construction company) in the multi-tenant system.
/// Note: Tenant entity does not inherit from BaseEntity as it doesn't have a TenantId.
/// </summary>
public class Tenant
{
    /// <summary>
    /// Unique identifier for the tenant.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Company name of the tenant.
    /// </summary>
    public string CompanyName { get; set; } = string.Empty;
    
    /// <summary>
    /// Subdomain for the tenant (e.g., "acme" for acme.constructiontracker.com).
    /// </summary>
    public string Subdomain { get; set; } = string.Empty;
    
    /// <summary>
    /// Primary contact email for the tenant.
    /// </summary>
    public string ContactEmail { get; set; } = string.Empty;
    
    /// <summary>
    /// Primary contact phone number for the tenant.
    /// </summary>
    public string? ContactPhone { get; set; }
    
    /// <summary>
    /// Business address of the tenant.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Subscription plan for the tenant (e.g., "free", "basic", "premium", "enterprise").
    /// </summary>
    public string SubscriptionPlan { get; set; } = "free";
    
    /// <summary>
    /// Indicates whether the tenant account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// UTC timestamp when the tenant was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// UTC timestamp when the tenant was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Navigation property for users belonging to this tenant.
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
