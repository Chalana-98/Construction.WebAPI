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
        return _currentTenantId;
    }
    
    public Guid GetCurrentUserId()
    {
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
