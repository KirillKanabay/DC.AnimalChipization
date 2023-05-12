using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Repositories;

public class AreaRepository : RepositoryBase<AreaEntity>, IAreaRepository
{
    public AreaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<bool> ExistsAsync(AreaFilter filter)
    {
        return GetQuery(filter).AnyAsync();
    }

    public Task<List<AreaEntity>> ListAsync(AreaFilter filter)
    {
        return GetQuery(filter).ToListAsync();
    }

    public Task<AreaEntity> FirstOrDefaultAsync(AreaFilter filter)
    {
        return GetQuery(filter).FirstOrDefaultAsync();
    }

    private IQueryable<AreaEntity> GetQuery(AreaFilter filter)
    {
        var query = base.GetQuery(filter);

        if (filter == null)
        {
            return query;
        }

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id.Equals(filter.Id));
        }

        return query;
    }
}