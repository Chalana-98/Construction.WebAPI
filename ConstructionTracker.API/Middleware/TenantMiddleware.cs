using System.Security.Claims;
using ConstructionTracker.Application.Common.Interfaces;

namespace ConstructionTracker.API.Middleware;

/// <summary>
/// Middleware to extract tenant and user information from JWT claims.
/// </summary>
public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantMiddleware> _logger;
    
    public TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            // Extract TenantId from JWT claims
            var tenantIdClaim = context.User.FindFirst("tenant_id") 
                ?? context.User.FindFirst("TenantId");
            
            if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out var tenantId))
            {
                tenantService.SetCurrentTenantId(tenantId);
                _logger.LogDebug("Tenant context set: {TenantId}", tenantId);
            }
            else
            {
                _logger.LogWarning("Authenticated user without valid tenant_id claim");
            }
            
            // Extract UserId from JWT claims
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier) 
                ?? context.User.FindFirst("sub")
                ?? context.User.FindFirst("user_id");
            
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                tenantService.SetCurrentUserId(userId);
                _logger.LogDebug("User context set: {UserId}", userId);
            }
        }
        
        await _next(context);
    }
}

/// <summary>
/// Extension method for registering the TenantMiddleware.
/// </summary>
public static class TenantMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantMiddleware>();
    }
}
