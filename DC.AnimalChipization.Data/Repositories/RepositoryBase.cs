using DC.AnimalChipization.Data.Common.Entities;
using DC.AnimalChipization.Data.Common.Filters;
using DC.AnimalChipization.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase
{
    protected readonly DbSet<TEntity> Set;
    protected readonly ApplicationDbContext Context;

    protected RepositoryBase(ApplicationDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Set = context.Set<TEntity>();
    }
    
    public virtual Task<List<TEntity>> ListAllAsync()
    {
        return Set.ToListAsync();
    }
    
    public virtual Task<TEntity> InsertAsync(TEntity entity)
    {
        Set.Add(entity);

        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);

        return Task.CompletedTask;
    }

    protected virtual IQueryable<TEntity> GetQuery(bool useTracking = false)
    {
        var query = Set.AsQueryable();

        if (!useTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    protected virtual IQueryable<TEntity> GetQuery<TFilter>(TFilter filter, bool useTracking = false)
        where TFilter : FilterBase<TEntity>
    {
        var query = GetQuery(useTracking);        

        if (filter.Includes.Any())
        {
            foreach (var propFunc in filter.Includes)
            {
                query = query.Include(propFunc);
            }
        }

        return query;
    }
}