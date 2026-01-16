using System.Linq.Expressions;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;

public interface IRepositoryQueryHandler<TEntity> where TEntity : BaseModel
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes); 

    Task<TEntity?> GetByIdAsync(Guid id,
        params Expression<Func<TEntity, object>>[]? includes);
    
    Task<IEnumerable<TEntity>> GetByInstitutionIdAsync(Guid institutionId);

    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[]? includes);

    Task<int> CreateAsync(TEntity entity);

    Task<int> UpdateAsync(TEntity entity);

    Task<int> DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}