using System.Data;
using ConstructionTracker.Application.Common.Interfaces;
using Npgsql;

namespace ConstructionTracker.Infrastructure.Data;

/// <summary>
/// Dapper context for raw SQL queries (CQRS - Query side).
/// </summary>
public class DapperContext : IDapperContext
{
    private readonly string _connectionString;
    
    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
