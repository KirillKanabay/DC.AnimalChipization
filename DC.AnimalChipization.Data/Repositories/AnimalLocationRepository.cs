using DC.AnimalChipization.Data.Common.Extensions;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DC.AnimalChipization.Data.Repositories;

public class AnimalLocationRepository : RepositoryBase<AnimalLocationEntity>, IAnimalLocationRepository
{
    public AnimalLocationRepository(ApplicationDbContext context) : base(context)
    {
    }

    private Dictionary<string, Expression<Func<AnimalLocationEntity, object>>> SortingColumns => new()
    {
        [nameof(AnimalLocationEntity.Id)] = x => x.Id
    };

    public Task<AnimalLocationEntity> FirstOrDefaultAsync(AnimalLocationFilter filter)
    {
        return GetQuery(filter).FirstOrDefaultAsync();
    }

    public Task<AnimalLocationEntity> GetByIdAsync(long id)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<AnimalLocationEntity>> ListAsync(AnimalLocationFilter filter, Paging paging)
    {
        return GetQuery(filter).ToPagedList(paging, SortingColumns);
    }

    public Task<List<AnimalLocationEntity>> GetAllLocationsAsync(DateTime startDate, DateTime endDate)
    {
        var query = GetQuery();
        
        query = query
            .Include(x => x.Animal)
                .ThenInclude(x => x.AnimalTypes)
            .Include(x => x.Animal)
                .ThenInclude(x => x.VisitedLocations)
            .Include(x => x.Location);

        return query.ToListAsync();
    }

    public override async Task DeleteAsync(AnimalLocationEntity entity)
    {
        var entityForDelete = await GetByIdAsync(entity.Id);

        Set.Remove(entityForDelete);
    }

    private IQueryable<AnimalLocationEntity> GetQuery(AnimalLocationFilter filter)
    {
        var query = base.GetQuery(filter);

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id);
        }

        if (filter.AnimalId.HasValue)
        {
            query = query.Where(x => x.AnimalId == filter.AnimalId);
        }

        if (filter.LocationPointId.HasValue)
        {
            query = query.Where(x => x.LocationPointId == filter.LocationPointId);
        }

        if (filter.StartDateTime.HasValue)
        {
            query = query.Where(x => x.VisitDateTime >= filter.StartDateTime);
        }

        if (filter.EndDateTime.HasValue)
        {
            query = query.Where(x => x.VisitDateTime <= filter.EndDateTime);
        }

        return query;
    }
}