using ConstructionTracker.Application.Common.Interfaces;
using ConstructionTracker.Domain.Common;
using ConstructionTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionTracker.Infrastructure.Data;

/// <summary>
/// Entity Framework Core database context for the Construction Tracker application.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ITenantService _tenantService;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
    }
    
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all entity configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Apply global query filter for multi-tenancy on all tenant entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(ApplyTenantQueryFilter), 
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .MakeGenericMethod(entityType.ClrType);
                
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }
    
    private void ApplyTenantQueryFilter<TEntity>(ModelBuilder modelBuilder) 
        where TEntity : class, ITenantEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == _tenantService.GetCurrentTenantId());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var tenantId = _tenantService.GetCurrentTenantId();
        
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = entry.Entity.Id == Guid.Empty ? Guid.NewGuid() : entry.Entity.Id;
                    entry.Entity.TenantId = tenantId;
                    entry.Entity.CreatedAt = now;
                    break;
                    
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    // Prevent TenantId from being modified
                    entry.Property(x => x.TenantId).IsModified = false;
                    break;
            }
        }
        
        // Handle Tenant entity separately (no TenantId field)
        foreach (var entry in ChangeTracker.Entries<Tenant>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = entry.Entity.Id == Guid.Empty ? Guid.NewGuid() : entry.Entity.Id;
                    entry.Entity.CreatedAt = now;
                    break;
                    
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    break;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}
