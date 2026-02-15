namespace ConstructionTracker.Domain.Common;

/// <summary>
/// Base entity class for all domain entities with multi-tenancy support.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Tenant identifier for multi-tenancy isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    
    /// <summary>
    /// UTC timestamp when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// UTC timestamp when the entity was last updated. Null if never updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
