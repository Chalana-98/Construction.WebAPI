using ConstructionTracker.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConstructionTracker.Infrastructure.Data;

/// <summary>
/// Design-time factory for creating ApplicationDbContext during migrations.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ConstructionTracker.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        
        // Create a mock tenant service for design-time operations
        var tenantService = new DesignTimeTenantService();
        
        return new ApplicationDbContext(optionsBuilder.Options, tenantService);
    }
}

/// <summary>
/// Mock tenant service for design-time operations (migrations).
/// </summary>
internal class DesignTimeTenantService : ITenantService
{
    private Guid _tenantId = Guid.Empty;
    private Guid _userId = Guid.Empty;
    
    public Guid GetCurrentTenantId() => _tenantId;
    public Guid GetCurrentUserId() => _userId;
    public void SetCurrentTenantId(Guid tenantId) => _tenantId = tenantId;
    public void SetCurrentUserId(Guid userId) => _userId = userId;
}
