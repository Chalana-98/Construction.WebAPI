using System.Data;

namespace ConstructionTracker.Application.Common.Interfaces;

/// <summary>
/// Interface for Dapper database connection factory.
/// </summary>
public interface IDapperContext
{
    /// <summary>
    /// Creates a new database connection for Dapper queries.
    /// </summary>
    IDbConnection CreateConnection();
}
