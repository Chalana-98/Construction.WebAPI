using ConstructionTracker.Application.Common.Interfaces;

namespace ConstructionTracker.Infrastructure.Services;

/// <summary>
/// Service for managing tenant context throughout the request lifecycle.
/// </summary>
public class TenantService : ITenantService
{
    private Guid _currentTenantId;
    private Guid _currentUserId;
    
    public Guid GetCurrentTenantId()
    {
        if (_currentTenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Tenant ID has not been set. Ensure the user is authenticated.");
        }
        
        return _currentTenantId;
    }
    
    public Guid GetCurrentUserId()
    {
        if (_currentUserId == Guid.Empty)
        {
            throw new InvalidOperationException("User ID has not been set. Ensure the user is authenticated.");
        }
        
        return _currentUserId;
    }
    
    public void SetCurrentTenantId(Guid tenantId)
    {
        _currentTenantId = tenantId;
    }
    
    public void SetCurrentUserId(Guid userId)
    {
        _currentUserId = userId;
    }
}
