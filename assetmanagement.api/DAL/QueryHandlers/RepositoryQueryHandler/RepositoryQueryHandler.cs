using System.Linq.Expressions;
using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.Entities.GeneralResponse;
using AssetManagement.Entities.Models; 

namespace AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;

public class RepositoryQueryHandler<TEntity>(ApplicationDbContext ctx) : IRepositoryQueryHandler<TEntity> where TEntity : BaseModel
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = ctx.Set<TEntity>();
        
        if (includes is { Length: > 0 })
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = ctx.Set<TEntity>();

        if (includes == null) return await query.FirstOrDefaultAsync(e => e.Id == id);
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<TEntity>> GetByInstitutionIdAsync(Guid institutionId)
    {
        if (!typeof(IInstitutionOwned).IsAssignableFrom(typeof(TEntity)))
            throw new InvalidOperationException(
                $"{typeof(TEntity).Name} does not implement {nameof(IInstitutionOwned)}");

        return await ctx.Set<TEntity>()
            .Where(e => ((IInstitutionOwned)e).InstitutionId == institutionId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = ctx.Set<TEntity>(); 
        if (includes == null) return  query.Where(predicate);
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => 
        await ctx.Set<TEntity>().AnyAsync(predicate);

    public async Task<int> CreateAsync(TEntity entity)
    {
        ctx.Set<TEntity>().Add(entity);
        return await ctx.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        ctx.Set<TEntity>().Update(entity);
        return await ctx.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await ctx.Set<TEntity>().FindAsync(id);
        if (entity == null) return 0;

        ctx.Set<TEntity>().Remove(entity);
        return await ctx.SaveChangesAsync();
    }

}