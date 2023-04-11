using DC.AnimalChipization.Data.Common.Extensions;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DC.AnimalChipization.Data.Repositories;

public class LocationRepository : RepositoryBase<LocationEntity>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext context) : base(context)
    {
    }

    private Dictionary<string, Expression<Func<LocationEntity, object>>> SortingColumns => new()
    {
        [nameof(LocationEntity.Id)] = x => x.Id
    };

    public Task<bool> ExistsAsync(LocationFilter filter)
    {
        return GetQuery(filter).AnyAsync();
    }

    public Task<LocationEntity> FirstOrDefaultAsync(LocationFilter filter)
    {
        return GetQuery(filter).FirstOrDefaultAsync();
    }

    public Task<List<LocationEntity>> ListAsync(LocationFilter filter, Paging paging)
    {
        return GetQuery(filter).ToPagedList(paging, SortingColumns);
    }

    public Task<LocationEntity> GetByIdAsync(long id)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id == id);
    }

    private IQueryable<LocationEntity> GetQuery(LocationFilter filter)
    {
        var query = base.GetQuery(filter);

        if (filter == null)
        {
            return query;
        }

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id);
        }

        if (filter.Latitude.HasValue)
        {
            query = query.Where(x => x.Latitude.Equals(filter.Latitude));
        }

        if (filter.Longitude.HasValue)
        {
            query = query.Where(x => x.Longitude.Equals(filter.Longitude));
        }

        return query;
    }
}