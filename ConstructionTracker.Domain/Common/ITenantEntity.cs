namespace ConstructionTracker.Domain.Common;

/// <summary>
/// Interface for entities that support multi-tenancy.
/// </summary>
public interface ITenantEntity
{
    Guid TenantId { get; set; }
}
