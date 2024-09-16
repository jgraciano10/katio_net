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
    public Task AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(TEntity entity)
    {
        throw new NotImplementedException();
        
    }

    public Task Delete(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> FindAsync(TId id)
    {
        
        return await _dbSet.FindAsync(id);
    }


    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, Book>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includeProperties = "")
    {
        var lista = await _dbSet.ToListAsync();
        return lista;
    }

    public async Task Update(TEntity entity)
    {
        
            _dbSet.Update(entity);      
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception)
            {
                
            }
        
    }
}