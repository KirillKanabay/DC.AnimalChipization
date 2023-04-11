using DC.AnimalChipization.Data.Common.Extensions;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Contracts;
using DC.AnimalChipization.Data.Repositories.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DC.AnimalChipization.Data.Repositories;

public class AnimalTypeRepository : RepositoryBase<AnimalTypeEntity>, IAnimalTypeRepository
{
    public AnimalTypeRepository(ApplicationDbContext context) : base(context)
    {
    }

    private Dictionary<string, Expression<Func<AnimalTypeEntity, object>>> SortingColumns => new()
    {
        [nameof(AnimalTypeEntity.Id)] = x => x.Id
    };

    public Task<AnimalTypeEntity> FirstOrDefaultAsync(AnimalTypeFilter filter)
    {
        return GetQuery(filter).FirstOrDefaultAsync();
    }

    public Task<List<AnimalTypeEntity>> ListAsync(AnimalTypeFilter filter, Paging paging)
    {
        return GetQuery(filter).ToPagedList(paging, SortingColumns);
    }

    public Task<AnimalTypeEntity> GetByTypeAsync(string type)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Type.Equals(type));
    }

    public Task<AnimalTypeEntity> GetByIdAsync(long id)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    private IQueryable<AnimalTypeEntity> GetQuery(AnimalTypeFilter filter)
    {
        var query = base.GetQuery(filter);

        if (filter == null)
        {
            return query;
        }

        if (filter.Ids.Count > 0)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }

        return query;
    }
}