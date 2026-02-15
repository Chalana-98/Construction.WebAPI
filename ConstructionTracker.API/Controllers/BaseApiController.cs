using Microsoft.AspNetCore.Mvc;

namespace ConstructionTracker.API.Controllers;

/// <summary>
/// Base controller with common functionality for all API controllers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// Gets the current user's ID from the JWT claims.
    /// </summary>
    protected Guid CurrentUserId
    {
        get
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub")
                ?? User.FindFirst("user_id");
            
            return userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId)
                ? userId
                : Guid.Empty;
        }
    }
    
    /// <summary>
    /// Gets the current tenant's ID from the JWT claims.
    /// </summary>
    protected Guid CurrentTenantId
    {
        get
        {
            var tenantIdClaim = User.FindFirst("tenant_id") 
                ?? User.FindFirst("TenantId");
            
            return tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out var tenantId)
                ? tenantId
                : Guid.Empty;
        }
    }
}
