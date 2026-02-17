using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionTracker.API.Controllers;

/// <summary>
/// Protected test controller to demonstrate JWT authentication.
/// </summary>
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : BaseApiController
{
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ILogger<ProjectsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all projects (protected endpoint - requires authentication).
    /// </summary>
    /// <returns>List of projects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> GetProjects()
    {
        var userId = User.FindFirst("UserId")?.Value;
        var tenantId = User.FindFirst("TenantId")?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        _logger.LogInformation(
            "GetProjects called by User: {UserId}, Tenant: {TenantId}, Role: {Role}", 
            userId, 
            tenantId, 
            role);

        // Mock data - will be replaced with actual database queries
        var projects = new[]
        {
            new
            {
                id = Guid.NewGuid(),
                name = "Downtown Office Building",
                status = "In Progress",
                tenantId = tenantId,
                createdBy = userId
            },
            new
            {
                id = Guid.NewGuid(),
                name = "Residential Complex Phase 1",
                status = "Planning",
                tenantId = tenantId,
                createdBy = userId
            }
        };

        var response = new
        {
            message = "This is a protected endpoint. Authentication successful!",
            user = new
            {
                userId = userId,
                email = email,
                role = role,
                tenantId = tenantId
            },
            projects = projects
        };

        return Ok(response);
    }

    /// <summary>
    /// Admin-only endpoint example.
    /// </summary>
    /// <returns>Admin data.</returns>
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<object> GetAdminData()
    {
        var userId = User.FindFirst("UserId")?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        _logger.LogInformation("Admin endpoint accessed by User: {UserId}", userId);

        return Ok(new
        {
            message = "This endpoint is only accessible to Admins",
            user = new
            {
                userId = userId,
                email = email,
                role = "Admin"
            }
        });
    }
}
