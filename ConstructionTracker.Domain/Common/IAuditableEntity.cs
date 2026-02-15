namespace ConstructionTracker.Domain.Common;

/// <summary>
/// Interface for auditable entities with creation and update timestamps.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
