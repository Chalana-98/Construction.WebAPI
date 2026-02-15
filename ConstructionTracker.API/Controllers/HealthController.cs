using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionTracker.API.Controllers;

/// <summary>
/// Health check endpoint for monitoring and load balancer probes.
/// </summary>
public class HealthController : BaseApiController
{
    private readonly ILogger<HealthController> _logger;
    
    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// Basic health check endpoint.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        _logger.LogDebug("Health check requested");
        return Ok(new 
        { 
            status = "healthy",
            timestamp = DateTime.UtcNow,
            version = "1.0.0"
        });
    }
    
    /// <summary>
    /// Detailed health check including database connectivity.
    /// </summary>
    [HttpGet("detailed")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDetailed(
        [FromServices] ConstructionTracker.Application.Common.Interfaces.IApplicationDbContext dbContext)
    {
        var dbHealthy = false;
        
        try
        {
            // Simple database connectivity check
            await dbContext.Tenants.CountAsync(new CancellationToken());
            dbHealthy = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database health check failed");
        }
        
        return Ok(new
        {
            status = dbHealthy ? "healthy" : "degraded",
            timestamp = DateTime.UtcNow,
            version = "1.0.0",
            checks = new
            {
                database = dbHealthy ? "healthy" : "unhealthy"
            }
        });
    }
}
