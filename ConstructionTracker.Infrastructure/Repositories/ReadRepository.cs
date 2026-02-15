using ConstructionTracker.Application.Common.Interfaces;
using ConstructionTracker.Domain.Common;
using Dapper;

namespace ConstructionTracker.Infrastructure.Repositories;

/// <summary>
/// Generic read repository using Dapper for optimized query performance.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly IDapperContext _dapperContext;
    private readonly string _tableName;
    
    public ReadRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
        _tableName = GetTableName();
    }
    
    private static string GetTableName()
    {
        // Convention: Table name is the plural form of the entity name
        var entityName = typeof(T).Name;
        return entityName.EndsWith("y") 
            ? $"{entityName[..^1]}ies" 
            : $"{entityName}s";
    }
    
    public async Task<T?> GetByIdAsync(Guid id, Guid tenantId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperContext.CreateConnection();
        
        var sql = $@"
            SELECT * FROM ""{_tableName}""
            WHERE ""Id"" = @Id AND ""TenantId"" = @TenantId";
        
        return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id, TenantId = tenantId });
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperContext.CreateConnection();
        
        var sql = $@"
            SELECT * FROM ""{_tableName}""
            WHERE ""TenantId"" = @TenantId
            ORDER BY ""CreatedAt"" DESC";
        
        return await connection.QueryAsync<T>(sql, new { TenantId = tenantId });
    }
    
    public async Task<IEnumerable<T>> GetPagedAsync(Guid tenantId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperContext.CreateConnection();
        
        var offset = (pageNumber - 1) * pageSize;
        
        var sql = $@"
            SELECT * FROM ""{_tableName}""
            WHERE ""TenantId"" = @TenantId
            ORDER BY ""CreatedAt"" DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        
        return await connection.QueryAsync<T>(sql, new { TenantId = tenantId, Offset = offset, PageSize = pageSize });
    }
    
    public async Task<int> CountAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperContext.CreateConnection();
        
        var sql = $@"
            SELECT COUNT(*) FROM ""{_tableName}""
            WHERE ""TenantId"" = @TenantId";
        
        return await connection.ExecuteScalarAsync<int>(sql, new { TenantId = tenantId });
    }
    
    public async Task<bool> ExistsAsync(Guid id, Guid tenantId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperContext.CreateConnection();
        
        var sql = $@"
            SELECT EXISTS(
                SELECT 1 FROM ""{_tableName}""
                WHERE ""Id"" = @Id AND ""TenantId"" = @TenantId
            )";
        
        return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id, TenantId = tenantId });
    }
}
