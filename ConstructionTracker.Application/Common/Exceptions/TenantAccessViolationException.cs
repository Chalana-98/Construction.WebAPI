namespace ConstructionTracker.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when attempting to access another tenant's data.
/// </summary>
public class TenantAccessViolationException : Exception
{
    public TenantAccessViolationException() 
        : base("Cross-tenant data access is not allowed.") { }
    
    public TenantAccessViolationException(string message) : base(message) { }
    
    public TenantAccessViolationException(string message, Exception innerException) 
        : base(message, innerException) { }
    
    public TenantAccessViolationException(Guid requestedTenantId, Guid actualTenantId)
        : base($"Attempted to access data from tenant {requestedTenantId} while authenticated as tenant {actualTenantId}.") { }
}
