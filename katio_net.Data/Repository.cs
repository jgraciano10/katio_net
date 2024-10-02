using System.Linq.Expressions;
using katio.Data.Models;
using katio_net.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace katio_net.Data;

public class Repository<TId, TEntity> : IRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    internal katioContext _context;
    internal DbSet<TEntity> _dbSet;

    public Repository(katioContext context){
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    public virtual async Task Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

   public virtual async Task Delete(TEntity entity)
    {
        if(_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public virtual async Task Delete(TId id)
    {
        TEntity entityToDelete = await _dbSet.FindAsync(id);
        Delete(entityToDelete);
    }

    public virtual async Task<TEntity> FindAsync(TId id)
    {
        
        return await _dbSet.FindAsync(id);
    }


    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if(filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(
            new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if(orderby is not null)
        {
            return await orderby(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }
}