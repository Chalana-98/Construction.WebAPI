using ConstructionTracker.Domain.Common;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Generic repository interface for read operations using Dapper (CQRS - Query side).
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public interface IReadRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Gets an entity by its identifier within the specified tenant.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, Guid tenantId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all entities for a specific tenant.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets entities with pagination for a specific tenant.
    /// </summary>
    Task<IEnumerable<T>> GetPagedAsync(Guid tenantId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the total count of entities for a specific tenant.
    /// </summary>
    Task<int> CountAsync(Guid tenantId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if an entity exists by its identifier within the specified tenant.
    /// </summary>
    Task<bool> ExistsAsync(Guid id, Guid tenantId, CancellationToken cancellationToken = default);
}
