using ConstructionTracker.Domain.Common;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Generic repository interface for write operations using EF Core (CQRS - Command side).
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public interface IWriteRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Removes an entity from the database.
    /// </summary>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Removes an entity by its identifier.
    /// </summary>
    Task DeleteByIdAsync(Guid id, Guid tenantId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds multiple entities to the database.
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
