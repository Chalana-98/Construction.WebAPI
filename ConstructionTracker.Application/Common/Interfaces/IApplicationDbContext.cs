using ConstructionTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Application database context interface for EF Core operations.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<User> Users { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
